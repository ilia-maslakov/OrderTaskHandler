using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SampleCamundaWorker.Ddos;
using SampleCamundaWorker.Services;
using Xunit;

namespace SampleCamundaWorker.Tests.Integration
{
    public class OrderControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IOrderService> _serviceMock = new();

        public OrderControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IOrderService>(_serviceMock.Object);
                });
            });
        }

        [Fact]
        public async Task CreateAndRejectOrderFlow()
        {
            // Arrange
            _serviceMock.Setup(s => s.CreateOrderAsync(It.IsAny<CreateOrderDto>())).ReturnsAsync("bk");
            _serviceMock.Setup(s => s.ApproveOrderAsync(It.IsAny<ApproveOrderDto>())).Returns(Task.CompletedTask);
            using var client = _factory.CreateClient();
            var createDto = new CreateOrderDto
            {
                Name = "order",
                OrderNumber = "1",
                OrderDate = DateTime.UtcNow,
                Remarks = string.Empty
            };
            var approveDto = new ApproveOrderDto { TaskId = "task", Approve = false, Remarks = "bad" };

            // Act
            var createResponse = await client.PostAsJsonAsync("/api/order/create", createDto);
            var businessKey = await createResponse.Content.ReadAsStringAsync();
            var approveResponse = await client.PostAsJsonAsync("/api/order/approve", approveDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);
            Assert.Equal("bk", businessKey.Trim('"'));
            Assert.Equal(HttpStatusCode.OK, approveResponse.StatusCode);
            _serviceMock.Verify(s => s.CreateOrderAsync(It.Is<CreateOrderDto>(d => d.Name == "order")), Times.Once);
            _serviceMock.Verify(s => s.ApproveOrderAsync(It.Is<ApproveOrderDto>(d => !d.Approve)), Times.Once);
        }

        [Fact]
        public async Task CreateAndApproveOrderFlow()
        {
            // Arrange
            _serviceMock.Setup(s => s.CreateOrderAsync(It.IsAny<CreateOrderDto>())).ReturnsAsync("bk2");
            _serviceMock.Setup(s => s.ApproveOrderAsync(It.IsAny<ApproveOrderDto>())).Returns(Task.CompletedTask);
            using var client = _factory.CreateClient();
            var createDto = new CreateOrderDto
            {
                Name = "order2",
                OrderNumber = "2",
                OrderDate = DateTime.UtcNow,
                Remarks = string.Empty
            };
            var approveDto = new ApproveOrderDto { TaskId = "task2", Approve = true, Remarks = string.Empty };

            // Act
            var createResponse = await client.PostAsJsonAsync("/api/order/create", createDto);
            var businessKey = await createResponse.Content.ReadAsStringAsync();
            var approveResponse = await client.PostAsJsonAsync("/api/order/approve", approveDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);
            Assert.Equal("bk2", businessKey.Trim('"'));
            Assert.Equal(HttpStatusCode.OK, approveResponse.StatusCode);
            _serviceMock.Verify(s => s.CreateOrderAsync(It.Is<CreateOrderDto>(d => d.Name == "order2")), Times.Once);
            _serviceMock.Verify(s => s.ApproveOrderAsync(It.Is<ApproveOrderDto>(d => d.Approve)), Times.Once);
        }
    }
}
