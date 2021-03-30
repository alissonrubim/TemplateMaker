using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using SuperNiceProject.Host.Lambda.Put.Presentations;
using SuperNiceProject.UseCases;
using SuperNiceProject.UseCases.Interfaces;
using Lambda.Base;
using SimpleInjector;
using SimpleInjector.Lifestyles;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SuperNiceProject.Host.Lambda.Put
{
    public class Function : FunctionFromPathAndBody<PathRequestModel, BodyRequestModel>
    {
        public Function() : base()
        {
            
        }

        public Function(Container container, bool IsUnitTest = false) : base(container, IsUnitTest)
        {
        }

        protected override async Task<APIGatewayProxyResponse> ProcessMessageAsync(PathRequestModel requestParameters, BodyRequestModel requestBody)
        {
            using var scope = AsyncScopedLifestyle.BeginScope(_container);
            var _useCase = scope.GetInstance<IUpdateOrder>();

            return await Task<APIGatewayProxyResponse>.Factory.StartNew(() => {
                _useCase.Update(requestParameters.OrderId, requestBody.ToModel());
                return ApiResponse(statusCode: HttpStatusCode.OK, body: null);
            });
        }

        public override void RegisterContainer(Container container)
        {
            container.Register<IUpdateOrder, UpdateOrder>();
        }
    }
}
