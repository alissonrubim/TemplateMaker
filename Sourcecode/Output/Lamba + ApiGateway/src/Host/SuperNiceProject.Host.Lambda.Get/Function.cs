using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using SuperNiceProject.Host.Lambda.Get.Presentations;
using SuperNiceProject.UseCases;
using SuperNiceProject.UseCases.Interfaces;
using Lambda.Base;
using SimpleInjector;
using SimpleInjector.Lifestyles;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SuperNiceProject.Host.Lambda.Get
{
    public class Function : FunctionFromPath<RequestModel>
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
            var _useCase = scope.GetInstance<IGetOrder>();

            return await Task<APIGatewayProxyResponse>.Factory.StartNew(() => {
                var Order = _useCase.Get(requestParameters.OrderId);
                if (Order == null)
                    return ApiResponse(statusCode: HttpStatusCode.NotFound, body: null);
                return ApiResponse(statusCode: HttpStatusCode.OK, body: Order);
            });
        }

        public override void RegisterContainer(SimpleInjector.Container container)
        {
            container.Register<IGetOrder, GetOrder>();
        }
    }
}
