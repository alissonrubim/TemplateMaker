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
using AutoFixture;
using {{SolutionName}}.Host.Lambda.{{Model.ModelName}}.Put;
using {{SolutionName}}.Host.Lambda.{{Model.ModelName}}.Put.Presentations;
using {{SolutionName}}.Models;
using System.Collections.Generic;

namespace {{SolutionName}}.Host.Lambda.Tests
{
    public class {{Model.ModelName}}PutFunctionTests
    {
        private readonly Mock<IUpdate{{Model.ModelName}}> _useCase;
        private readonly Function _function;
        private readonly Fixture _fixure;

        const int VALID_ID = 7784;
        const string HTTPMETHOD = "PUT";
        const string HTTPPATH = "/{Id}";

        public {{Model.ModelName}}PutFunctionTests()
        {
            _useCase = new Mock<IUpdate{{Model.ModelName}}>();

            var container = new Container();
            container.RegisterInstance(_useCase.Object);

            var logger = new Mock<ILogger>();
            container.RegisterInstance(new MonitoringEvents(logger.Object, new AwsLambdaDatadogMetrics()));
            _function = new Function(container, true);

            _fixure = new Fixture();
        }

        [Fact]
        public void FunctionHandler_WithValidRequest_WillReturnCreated()
        {
            var request = RequestHelper.CreateRequest(HTTPMETHOD, HTTPPATH, new Dictionary<string, string> {
                { "{{Model.KeyField.Name}}", VALID_ID.ToString() }
            }, _fixure.Create<BodyRequestModel>());
            var task = _function.FunctionHandler(request, null);

            task.Result.Should().NotBeNull();
            task.Result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            _useCase.Verify(x => x.Update(VALID_ID, It.IsAny<{{Model.ModelName}}Model>()), Times.Once);
        }

        [Fact]
        public void FunctionHandler_WithInvalidRequest_WillReturnBadRequest()
        {

            /*_fixure.Customize<BodyRequestModel>(builder =>
                builder.With(x => x.OrderId, -999)
                       .With(x => x.InvoiceId, -999)
            );

            var request = RequestHelper.CreateRequest(HTTPMETHOD, HTTPPATH, new Dictionary<string, string> {
                { "{{Model.KeyField.Name}}", {{Model.KeyField.Name}}.ToString() }
            }, _fixure.Create<BodyRequestModel>());
            var task = _function.FunctionHandler(request, null);

            task.Result.Should().NotBeNull();
            task.Result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            _useCase.Verify(x => x.Update({{Model.KeyField.Name}}, It.IsAny<{{Model.ModelName}}Model>()), Times.Never);*/
        }
    }
}