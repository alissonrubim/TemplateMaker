using Coolblue.Utilities.MonitoringEvents;
using FluentAssertions;
using Moq;
using Serilog;
using Xunit;
using SimpleInjector;
using Coolblue.Utilities.MonitoringEvents.Aws.Lambda.Datadog;
using System.Net;
using SuperNiceProject.Host.Lambda.Tests.Helpers;
using SuperNiceProject.UseCases.Interfaces;
using SuperNiceProject.Host.Lambda.Get;
using System.Collections.Generic;

namespace SuperNiceProject.Host.Lambda.Tests
{
    public class GetFunctionTests
    {
        private readonly Mock<IGetOrder> _useCase;
        private readonly Function _function;

        const int INVITO_TO_PAYID = 999;
        const string HTTPMETHOD = "GET";
        const string HTTPPATH = "/{Id}";

        public GetFunctionTests()
        {
            _useCase = new Mock<IGetOrder>();

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
                { "OrderId", INVITO_TO_PAYID.ToString() }
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