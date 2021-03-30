using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using {{SolutionName}}.UseCases;
using {{SolutionName}}.UseCases.Interfaces;
using Lambda.Base;
using SimpleInjector;
using SimpleInjector.Lifestyles;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace {{SolutionName}}.Host.Lambda.{{Model.ModelName}}.GetAll
{
    public class Function : FunctionFromPath
    {
        public Function() : base()
        {
           
        }

        public Function(Container container, bool IsUnitTest = false) : base(container, IsUnitTest)
        {
        }

        protected override async Task<APIGatewayProxyResponse> ProcessMessageAsync()
        {
            using var scope = AsyncScopedLifestyle.BeginScope(_container);
            var _useCase = scope.GetInstance<IGetAll{{Model.ModelName}}>();

            return await Task<APIGatewayProxyResponse>.Factory.StartNew(() => {
                return ApiResponse(statusCode: HttpStatusCode.OK, body: _useCase.GetAll());
            });
        }

        public override void RegisterContainer(SimpleInjector.Container container)
        {
            container.Register<IGetAll{{Model.ModelName}}, GetAll{{Model.ModelName}}>();
        }
    }
}
