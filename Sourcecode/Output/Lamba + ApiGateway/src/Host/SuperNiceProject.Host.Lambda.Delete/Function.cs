using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using SuperNiceProject.Host.Lambda.Delete.Presentations;
using SuperNiceProject.UseCases;
using SuperNiceProject.UseCases.Interfaces;
using Lambda.Base;
using SimpleInjector;
using SimpleInjector.Lifestyles;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SuperNiceProject.Host.Lambda.Delete
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
            var _useCase = scope.GetInstance<IDeleteOrder>();

            return await Task<APIGatewayProxyResponse>.Factory.StartNew(() => {
                _useCase.Delete(requestParameters.OrderId);
                return ApiResponse(statusCode: HttpStatusCode.OK, body: null);
            });
        }

        public override void RegisterContainer(SimpleInjector.Container container)
        {
            container.Register<IDeleteOrder, DeleteOrder>();
        }
    }
}
