using System;
using Moq;
using Use_Wheels.Models.DTO;
using Use_Wheels.Services.IServices;

namespace UseWheels.Tests
{
    public class OrderControllerTests
    {
        private OrderController _orderControllerMock;
        private Mock<IOrderServices> _orderServicesMock;

        public OrderControllerTests()
        {
            _orderServicesMock = new Mock<IOrderServices>();

        }

        [Fact]
        public async Task CreateOrderTest()
        {
            // Arrange
            OrderDTO orderDTO = new OrderDTO() { Email = "user1@gmail.com", Payment_Type = "cash", Vehicle_No = "TN 67 RD 3412" };
            _orderServicesMock.Setup(order => order.CreateOrder(orderDTO));
            _orderControllerMock = new OrderController(_orderServicesMock.Object);

            // Act
            var result = await _orderControllerMock.CreateOrder(orderDTO);

            // Assert
            _orderServicesMock.Verify(order => order.CreateOrder(orderDTO), Times.Once);
        }
    } 
}

