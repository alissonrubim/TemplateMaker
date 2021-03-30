using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Coolblue.Utilities.MonitoringEvents;
using Lambda.Base.Exceptions;
using Lambda.Base.Presentations;
using Newtonsoft.Json;
using SimpleInjector;

namespace Lambda.Base
{
    public interface ILambdaFunction
    {
        Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context);
    }

    public abstract class BaseFunction
    {
        protected readonly Container _container;

        protected readonly MonitoringEvents _monitoringEvents;

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public BaseFunction() : this(Bootstrapper.CreateDefaultApplication(Bootstrapper.GetLambdaSettings()))
        {
        }

        public BaseFunction(Container container, bool IsUnitTest = false)
        {
            if (!IsUnitTest)
            {
                RegisterContainer(container);
            }

            container.Verify();
            _container = container;
            _monitoringEvents = container.GetInstance<MonitoringEvents>();
        }

        public abstract void RegisterContainer(Container container);

        protected APIGatewayProxyResponse ApiResponse(HttpStatusCode statusCode, string message)
        {
            return new APIGatewayProxyResponse {
                StatusCode = (int)statusCode,
                Body = message
            };
        }

        protected APIGatewayProxyResponse ApiResponse(HttpStatusCode statusCode, object body)
        {
            return new APIGatewayProxyResponse {
                StatusCode = (int)statusCode,
                Body = JsonConvert.SerializeObject(body)
            };
        }

        protected static Guid GetCorrelationId()
        {
            return Guid.NewGuid();
        }

        protected T MapFromPath<T>(APIGatewayProxyRequest request)
        {
            if (request == null)
                throw new InvalidRequestException("Empty request");

            var dictonary = request.PathParameters;
            if (dictonary.Values.Count == 0)
                return default;

            var type = typeof(T);
            var obj = Activator.CreateInstance<T>();
            var properties = type.GetProperties();
            foreach(var property in properties)
            {
                var param = dictonary.Where(x => x.Key.ToLower().Equals(property.Name.ToLower())).FirstOrDefault();
                if (param.Value != null)
                    property.SetValue(obj, Convert.ChangeType(param.Value, property.PropertyType));
            }
            return obj;
        }

        protected T MapFromBody<T>(APIGatewayProxyRequest request)
        {
            if (request == null)
                throw new InvalidRequestException("Empty request");

            if (string.IsNullOrEmpty(request.Body))
                throw new InvalidRequestException("Missing request body");

            try
            {
                return JsonConvert.DeserializeObject<T>(request.Body);
            }
            catch (Exception e)
            {
                throw new InvalidRequestException(e.Message);
            }
        }
    }
    
    public abstract class FunctionFromPath : BaseFunction
    {
        public FunctionFromPath(Container container, bool IsUnitTest = false) : base(container, IsUnitTest)
        {
            //Constructor used for tests with custom container
        }
        public FunctionFromPath() : base()
        {

        }

        public virtual async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                _monitoringEvents.Logger.Information("Processed message {request}", request);
                var correlationId = GetCorrelationId();
                using (_monitoringEvents.LogContext.PushProperty(new LogContextProperty("CorrelationId", correlationId.ToString())))
                {
                    return await ProcessMessageAsync();
                }
            }
            catch (InvalidRequestException ex)
            {
                _monitoringEvents.Logger.Information("Invalid request {request}", request);
                return ApiResponse(statusCode: HttpStatusCode.BadRequest, message: ex.Message);
            }
            catch (Exception ex)
            {
                _monitoringEvents.Logger.Information("An error ocurred for the request {request}", request);
                return ApiResponse(statusCode: HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        protected abstract Task<APIGatewayProxyResponse> ProcessMessageAsync();
    }

    public abstract class FunctionFromPath<TFromPath>: BaseFunction where TFromPath : IRequestModel
    {
        public FunctionFromPath(Container container, bool IsUnitTest = false) : base(container, IsUnitTest)
        {
            //Constructor used for tests with custom container
        }
        public FunctionFromPath() : base()
        {

        }

        public virtual async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                _monitoringEvents.Logger.Information("Processed message {request}", request);
                var correlationId = GetCorrelationId();
                using (_monitoringEvents.LogContext.PushProperty(new LogContextProperty("CorrelationId", correlationId.ToString())))
                {
                    var paramsPath = MapFromPath<TFromPath>(request);
                    paramsPath.Validate();
                    return await ProcessMessageAsync(paramsPath);
                }
            }
            catch (InvalidRequestException ex)
            {
                _monitoringEvents.Logger.Information("Invalid request {request}", request);
                return ApiResponse(statusCode: HttpStatusCode.BadRequest, message: ex.Message);
            }
            catch (Exception ex)
            {
                _monitoringEvents.Logger.Information("An error ocurred for the request {request}", request);
                return ApiResponse(statusCode: HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        protected abstract Task<APIGatewayProxyResponse> ProcessMessageAsync(TFromPath requestParameters);
    }

    public abstract class FunctionFromPathAndBody<TFromPath, TFromBody>: BaseFunction, ILambdaFunction where TFromPath : IRequestModel
                                                                       where TFromBody : IRequestModel
    {
        public FunctionFromPathAndBody(Container container, bool IsUnitTest = false) : base(container, IsUnitTest)
        {
            //Constructor used for tests with custom container
        }
        public FunctionFromPathAndBody() : base()
        {

        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                _monitoringEvents.Logger.Information("Processed message {request}", request);
                var correlationId = GetCorrelationId();
                using (_monitoringEvents.LogContext.PushProperty(new LogContextProperty("CorrelationId", correlationId.ToString())))
                {
                    var requestBody = MapFromBody<TFromBody>(request);
                    requestBody.Validate();
                    var paramsPath = MapFromPath<TFromPath>(request);
                    paramsPath.Validate();
                    return await ProcessMessageAsync(paramsPath, requestBody);
                }
            }
            catch(InvalidRequestException ex)
            {
                _monitoringEvents.Logger.Information("Invalid request {request}", request);
                return ApiResponse(statusCode: HttpStatusCode.BadRequest, message: ex.Message);
            }catch(Exception ex)
            {
                _monitoringEvents.Logger.Information("An error ocurred for the request {request}", request);
                return ApiResponse(statusCode: HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        protected abstract Task<APIGatewayProxyResponse> ProcessMessageAsync(TFromPath requestParameters, TFromBody requestBody);
    }

    public abstract class FunctionFromBody<TFromBody> : BaseFunction, ILambdaFunction where TFromBody : IRequestModel
    {
        public FunctionFromBody(Container container, bool IsUnitTest = false) : base(container, IsUnitTest)
        {
            //Constructor used for tests with custom container
        }
        public FunctionFromBody() : base()
        {
            
        }

        internal TFromBody GetBody(APIGatewayProxyRequest request)
        {
            if (request == null)
                throw new InvalidRequestException("Empty request");

            if (string.IsNullOrEmpty(request.Body))
                throw new InvalidRequestException("Missing request body");

            try
            {
                return JsonConvert.DeserializeObject<TFromBody>(request.Body);
            }
            catch (Exception e)
            {
                throw new InvalidRequestException(e.Message);
            }
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                _monitoringEvents.Logger.Information("Processed message {request}", request);
                var correlationId = GetCorrelationId();
                using (_monitoringEvents.LogContext.PushProperty(new LogContextProperty("CorrelationId", correlationId.ToString())))
                {
                    var requestBody = MapFromBody<TFromBody>(request);
                    requestBody.Validate();
                    return await ProcessMessageAsync(requestBody);
                }
            }
            catch (InvalidRequestException ex)
            {
                _monitoringEvents.Logger.Information("Invalid request {request}", request);
                return ApiResponse(statusCode: HttpStatusCode.BadRequest, message: ex.Message);
            }
            catch (Exception ex)
            {
                _monitoringEvents.Logger.Information("An error ocurred for the request {request}", request);
                return ApiResponse(statusCode: HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        protected abstract Task<APIGatewayProxyResponse> ProcessMessageAsync(TFromBody requestBody);
    }
}