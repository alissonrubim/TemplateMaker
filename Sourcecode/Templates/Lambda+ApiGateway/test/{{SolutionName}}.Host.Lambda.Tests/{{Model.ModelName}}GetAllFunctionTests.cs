using Coolblue.Utilities.MonitoringEvents;
using FluentAssertions;
using Moq;
using Serilog;
using Xunit;
using SimpleInjector;
using Coolblue.Utilities.MonitoringEvents.Aws.Lambda.Datadog;
using System.Net;
using {{SolutionName}}.Host.Lambda.Tests.Helpers;
using {{SolutionName}}.UseCases.Interfaces;
using {{SolutionName}}.Host.Lambda.{{Model.ModelName}}.GetAll;
using System.Collections.Generic;

namespace {{SolutionName}}.Host.Lambda.Tests
{
    public class {{Model.ModelName}}GetAllFunctionTests
    {
        private readonly Mock<IGet{{Model.ModelName}}> _useCase;
        private readonly Function _function;

        const int INVITO_TO_PAYID = 999;
        const string HTTPMETHOD = "GET";
        const string HTTPPATH = "/{Id}";

        public {{Model.ModelName}}GetAllFunctionTests()
        {
            _useCase = new Mock<IGet{{Model.ModelName}}>();

            var container = new Container();
            container.RegisterInstance(_useCase.Object);

            var logger = new Mock<ILogger>();
            container.RegisterInstance(new MonitoringEvents(logger.Object, new AwsLambdaDatadogMetrics()));
            _function = new Function(container, true);
        }

        [Fact]
        public void FunctionHandler_WithValidRequest_WillReturnNotFound()
        {
            var request = RequestHelper.CreateRequest(HTTPMETHOD, HTTPPATH, new Dictionary<string, string> {
                { "{{Model.KeyField.Name}}", INVITO_TO_PAYID.ToString() }
            });
            var task = _function.FunctionHandler(request, null);

            task.Result.Should().NotBeNull();
            task.Result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            _useCase.Verify(x => x.Get(INVITO_TO_PAYID), Times.Once);
        }

        [Fact]
        public void FunctionHandler_WithInvalidRequest_WillReturnBadRequest()
        {
            var request = RequestHelper.CreateRequest(HTTPMETHOD, HTTPPATH, new Dictionary<string, string> {
                { "AnyString", INVITO_TO_PAYID.ToString() }
            });
            var task = _function.FunctionHandler(request, null);

            task.Result.Should().NotBeNull();
            task.Result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            _useCase.Verify(x => x.Get(INVITO_TO_PAYID), Times.Never);
        }
    }
}