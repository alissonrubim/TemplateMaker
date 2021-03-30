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
using SuperNiceProject.Host.Lambda.Post;
using SuperNiceProject.Host.Lambda.Post.Presentations;
using SuperNiceProject.Models;

namespace SuperNiceProject.Host.Lambda.Tests
{
    public class PostFunctionTests
    {
        private readonly Mock<ICreateOrder> _useCase;
        private readonly Function _function;
        private readonly Fixture _fixure;

        const string HTTPMETHOD = "POST";
        const string HTTPPATH = "/";

        public PostFunctionTests()
        {
            _useCase = new Mock<ICreateOrder>();

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
            /*_fixure.Customize<RequestModel>(builder =>
               builder.With(x => x.InvoiceId, -999)
                      .With(x => x.OrderId, 1234)
            );

            var request = RequestHelper.CreateRequest(HTTPMETHOD, HTTPPATH, null, _fixure.Create<RequestModel>());
            var task = _function.FunctionHandler(request, null);

            task.Result.Should().NotBeNull();
            task.Result.StatusCode.Should().Be((int)HttpStatusCode.Created);
            _useCase.Verify(x => x.Create(It.IsAny<OrderModel>()), Times.Once);*/
        }

        [Fact]
        public void FunctionHandler_WithInvalidRequest_WillReturnBadRequest()
        {
            /*_fixure.Customize<RequestModel>(builder =>
                builder.With(x => x.InvoiceId, -999)
                       .With(x => x.OrderId, -999)
            );

            var request = RequestHelper.CreateRequest(HTTPMETHOD, HTTPPATH, null, _fixure.Create<RequestModel>());
            var task = _function.FunctionHandler(request, null);

            task.Result.Should().NotBeNull();
            task.Result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            _useCase.Verify(x => x.Create(It.IsAny<OrderModel>()), Times.Never);*/
        }
    }
}