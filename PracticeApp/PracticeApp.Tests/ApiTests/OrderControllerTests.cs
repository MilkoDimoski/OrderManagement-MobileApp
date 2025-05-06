using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PracticeApp.API.Controllers;
using PracticeApp.Services.Interfaces;
using PracticeApp.Services.Models;
using Xunit;

namespace PracticeApp.Tests.ApiTests
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task GetAllOrders_ReturnsOkResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var orders = new List<Order> { new Order { OrderId = 1 }, new Order { OrderId = 2 } };
            mockService.GetAllOrders().Returns(orders);
            var controller = new OrderController(mockService);

            var result = await controller.GetAllOrders();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnOrders = Assert.IsType<List<Order>>(okResult.Value);
            Assert.Equal(2, returnOrders.Count);
        }

        [Fact]
        public async Task GetOrderById_ReturnsOkResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var order = new Order { OrderId = 1 };
            mockService.GetOrderById(1).Returns(order);
            var controller = new OrderController(mockService);

            var result = await controller.GetOrderById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(1, returnOrder.OrderId);
        }

        [Fact]
        public async Task GetOrdersByUserId_ReturnsOkResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var orders = new List<Order> { new Order { OrderId = 1 } };
            mockService.GetOrdersByUser(1).Returns(orders);
            var controller = new OrderController(mockService);

            var result = await controller.GetOrdersByUserId(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnOrders = Assert.IsType<List<Order>>(okResult.Value);
            Assert.Single(returnOrders);
        }

        [Fact]
        public async Task AddOrder_ReturnsCreatedResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var order = new Order { OrderId = 1 };
            mockService.AddOrder(order).Returns(1);
            var controller = new OrderController(mockService);

            var result = await controller.AddOrder(order);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnOrder = Assert.IsType<Order>(createdResult.Value);
            Assert.Equal(1, returnOrder.OrderId);
        }

        [Fact]
        public async Task UpdateOrder_ReturnsOkResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var order = new Order { OrderId = 1 };
            mockService.GetOrderById(1).Returns(order);
            var controller = new OrderController(mockService);

            var result = await controller.UpdateOrder(order);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(1, returnOrder.OrderId);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsOkResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var order = new Order { OrderId = 1 };
            mockService.GetOrderByOrderNumber(1).Returns(order);
            var controller = new OrderController(mockService);

            var result = await controller.DeleteOrder(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(1, returnOrder.OrderId);
        }

        [Fact]
        public async Task GetAllOrderDetails_ReturnsOkResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var details = new List<OrderDetail> { new OrderDetail { OrderDetailId = 1 } };
            mockService.GetAllOrderDetails().Returns(details);
            var controller = new OrderController(mockService);

            var result = await controller.GetAllOrderDetails();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDetails = Assert.IsType<List<OrderDetail>>(okResult.Value);
            Assert.Single(returnDetails);
        }

        [Fact]
        public async Task GetOrderDetailById_ReturnsOkResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var detail = new OrderDetail { OrderDetailId = 1 };
            mockService.GetOrderDetailById(1).Returns(detail);
            var controller = new OrderController(mockService);

            var result = await controller.GetOrderDetailById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDetail = Assert.IsType<OrderDetail>(okResult.Value);
            Assert.Equal(1, returnDetail.OrderDetailId);
        }

        [Fact]
        public async Task GetOrderDetailsByOrderNumber_ReturnsOkResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var details = new List<OrderDetail> { new OrderDetail { OrderDetailId = 1 } };
            mockService.GetOrderDetailsByOrderNumber(1).Returns(details);
            var controller = new OrderController(mockService);

            var result = await controller.GetOrderDetailsByOrderNumber(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDetails = Assert.IsType<List<OrderDetail>>(okResult.Value);
            Assert.Single(returnDetails);
        }

        [Fact]
        public async Task AddOrderDetail_ReturnsCreatedResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var detail = new OrderDetail { OrderDetailId = 1, OrderNumber = 1 };
            mockService.AddOrderDetail(detail).Returns(1);
            var controller = new OrderController(mockService);

            var result = await controller.AddOrderDetail(detail);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnDetail = Assert.IsType<OrderDetail>(createdResult.Value);
            Assert.Equal(1, returnDetail.OrderDetailId);
        }

        [Fact]
        public async Task UpdateOrderDetail_ReturnsOkResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var detail = new OrderDetail { OrderDetailId = 1 };
            mockService.GetOrderDetailById(1).Returns(detail);
            var controller = new OrderController(mockService);

            var result = await controller.UpdateOrderDetail(detail);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDetail = Assert.IsType<OrderDetail>(okResult.Value);
            Assert.Equal(1, returnDetail.OrderDetailId);
        }

        [Fact]
        public async Task DeleteOrderDetail_ReturnsOkResult()
        {
            var mockService = Substitute.For<IOrderService>();
            var detail = new OrderDetail { OrderDetailId = 1 };
            mockService.GetOrderDetailById(1).Returns(detail);
            var controller = new OrderController(mockService);

            var result = await controller.DeleteOrderDetail(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDetail = Assert.IsType<OrderDetail>(okResult.Value);
            Assert.Equal(1, returnDetail.OrderDetailId);
        }
    }
}
