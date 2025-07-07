using System;
using System.Threading.Tasks;
using Moq;
using SampleCamundaWorker.Ddos;
using SampleCamundaWorker.Infrastructure.Camunda.Models;
using SampleCamundaWorker.Services;
using Xunit;

namespace SampleCamundaWorker.Tests.Unit
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateOrderAsync_StartsProcessAndCompletesTask()
        {
            // Arrange
            var camundaMock = new Mock<ICamundaClient>();
            var service = new OrderService(camundaMock.Object);
            var dto = new CreateOrderDto
            {
                Name = "Test order",
                OrderNumber = "123",
                OrderDate = DateTime.UtcNow,
                Remarks = "remark"
            };
            string? capturedBusinessKey = null;
            camundaMock
                .Setup(c => c.StartProcessInstanceAsync("process-t", It.IsAny<string>(), It.IsAny<CamundaVariables>()))
                .Callback<string, string, CamundaVariables>((_, bk, _) => capturedBusinessKey = bk)
                .Returns(Task.CompletedTask);
            camundaMock
                .Setup(c => c.CompleteUserTaskAsync(It.IsAny<string>(), "order/create", It.IsAny<CamundaVariables>()))
                .Returns(Task.CompletedTask);

            // Act
            var businessKey = await service.CreateOrderAsync(dto);

            // Assert
            Assert.Equal(capturedBusinessKey, businessKey);
            camundaMock.Verify(c => c.StartProcessInstanceAsync("process-t", businessKey, It.IsAny<CamundaVariables>()), Times.Once);
            camundaMock.Verify(c => c.CompleteUserTaskAsync(businessKey, "order/create", It.IsAny<CamundaVariables>()), Times.Once);
        }

        [Fact]
        public async Task EditOrderAsync_CompletesTask()
        {
            // Arrange
            var camundaMock = new Mock<ICamundaClient>();
            var service = new OrderService(camundaMock.Object);
            var dto = new EditOrderDto
            {
                BusinessKey = "bk",
                Name = "edit"
            };
            camundaMock
                .Setup(c => c.CompleteUserTaskAsync("bk", "order/edit", It.IsAny<CamundaVariables>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await service.EditOrderAsync(dto);

            // Assert
            camundaMock.Verify();
        }

        [Fact]
        public async Task ApproveOrderAsync_CompletesTask()
        {
            // Arrange
            var camundaMock = new Mock<ICamundaClient>();
            var service = new OrderService(camundaMock.Object);
            var dto = new ApproveOrderDto { TaskId = "task", Approve = true, Remarks = "" };
            camundaMock
                .Setup(c => c.CompleteUserTaskAsync("task", "order/approve", It.IsAny<CamundaVariables>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await service.ApproveOrderAsync(dto);

            // Assert
            camundaMock.Verify();
        }
    }
}
