using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using SuperNiceProject.Host.Lambda.Post.Presentations;
using SuperNiceProject.UseCases;
using SuperNiceProject.UseCases.Interfaces;
using Lambda.Base;
using SimpleInjector;
using SimpleInjector.Lifestyles;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SuperNiceProject.Host.Lambda.Post
{
    public class Function : FunctionFromBody<RequestModel>
    {
        public Function() : base()
        {
            
        }

        public Function(Container container, bool IsUnitTest = false) : base(container, IsUnitTest)
        {
        }

        protected override async Task<APIGatewayProxyResponse> ProcessMessageAsync(RequestModel requestParameters)
        {
            using var scope = AsyncScopedLifestyle.BeginScope(_container);
            var _useCase = scope.GetInstance<ICreateOrder>();

            return await Task<APIGatewayProxyResponse>.Factory.StartNew(() => {
                var OrderId = _useCase.Create(requestParameters.ToModel());
                return ApiResponse(statusCode: HttpStatusCode.Created, body: new ResponseModel {
                    OrderId = OrderId
                });
            });
        }

        public override void RegisterContainer(Container container)
        {
            container.Register<ICreateOrder, CreateOrder>();
        }
    }
}
