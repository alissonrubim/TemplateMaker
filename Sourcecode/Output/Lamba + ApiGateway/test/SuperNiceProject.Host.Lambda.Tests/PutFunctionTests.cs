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
using AutoFixture;
using SuperNiceProject.Host.Lambda.Put;
using SuperNiceProject.Host.Lambda.Put.Presentations;
using SuperNiceProject.Models;
using System.Collections.Generic;

namespace SuperNiceProject.Host.Lambda.Tests
{
    public class PutFunctionTests
    {
        private readonly Mock<IUpdateOrder> _useCase;
        private readonly Function _function;
        private readonly Fixture _fixure;

        const int VALID_ID = 7784;
        const string HTTPMETHOD = "PUT";
        const string HTTPPATH = "/{Id}";

        public PutFunctionTests()
        {
            _useCase = new Mock<IUpdateOrder>();

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
                { "OrderId", VALID_ID.ToString() }
            }, _fixure.Create<BodyRequestModel>());
            var task = _function.FunctionHandler(request, null);

            task.Result.Should().NotBeNull();
            task.Result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            _useCase.Verify(x => x.Update(VALID_ID, It.IsAny<OrderModel>()), Times.Once);
        }

        [Fact]
        public void FunctionHandler_WithInvalidRequest_WillReturnBadRequest()
        {

            /*_fixure.Customize<BodyRequestModel>(builder =>
                builder.With(x => x.OrderId, -999)
                       .With(x => x.InvoiceId, -999)
            );

            var request = RequestHelper.CreateRequest(HTTPMETHOD, HTTPPATH, new Dictionary<string, string> {
                { "OrderId", OrderId.ToString() }
            }, _fixure.Create<BodyRequestModel>());
            var task = _function.FunctionHandler(request, null);

            task.Result.Should().NotBeNull();
            task.Result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            _useCase.Verify(x => x.Update(OrderId, It.IsAny<OrderModel>()), Times.Never);*/
        }
    }
}