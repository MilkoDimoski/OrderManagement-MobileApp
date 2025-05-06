using AutoMapper;
using NSubstitute;
using PracticeApp.Domain.Models;
using PracticeApp.Repository.Interfaces;
using PracticeApp.Services;
using PracticeApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PracticeApp.Tests.ServiceTests
{
    public class OrderServiceTests
    {
        private readonly IOrderRepository _orderRepository = Substitute.For<IOrderRepository>();
        private readonly IMapper _mapperMock = Substitute.For<IMapper>();

        [Fact]
        public async Task GetOrderById_ShouldReturnOrder()
        {
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 1,
                OrderStatus = "active",
                UserId = 1
            };
            var order = new Order
            {
                OrderStatus = "active",
                OrderNumber = 1,
                OrderId = 1,
                UserId = 1
            };
            _orderRepository.GetOrderById(Arg.Any<int>()).Returns(orderDto);
            _mapperMock.Map<Order>(orderDto).Returns(order);
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.GetOrderById(orderDto.OrderId);
            Assert.NotNull(result);
            Assert.Equal(orderDto.OrderStatus, result.OrderStatus);
            Assert.Equal(orderDto.OrderNumber, result.OrderNumber);
            Assert.Equal(orderDto.OrderId, result.OrderId);
            Assert.Equal(orderDto.UserId, result.UserId);
        }
        [Fact]
        public async Task GetOrderById_ShouldThrowException_WhenOrderNotFound()
        {
            _orderRepository.GetOrderById(Arg.Any<int>()).Returns((OrderDto)null);
            var sut = new OrderService(_orderRepository, _mapperMock);
            await Assert.ThrowsAsync<Exception>(async () => await sut.GetOrderById(1));
        }

        [Fact]
        public async Task GerOrderByOrderNumber_ShouldReturnOrder()
        {
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 1,
                OrderStatus = "active",
                UserId = 1
            };
            var order = new Order
            {
                OrderStatus = "active",
                OrderNumber = 1,
                OrderId = 1,
                UserId = 1
            };
            _orderRepository.GetOrderByOrderNumber(Arg.Any<int>()).Returns(orderDto);
            _mapperMock.Map<Order>(orderDto).Returns(order);
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.GetOrderByOrderNumber(1);
            Assert.NotNull(result);
            Assert.Equal(orderDto.OrderStatus, result.OrderStatus);
            Assert.Equal(orderDto.OrderNumber, result.OrderNumber);
            Assert.Equal(orderDto.OrderId, result.OrderId);
            Assert.Equal(orderDto.UserId, result.UserId);
        }

        [Fact]
        public async Task GetOrderByOrderNumber_ShouldThrowException_WhenOrderNotFound()
        {
            _orderRepository.GetOrderByOrderNumber(Arg.Any<int>()).Returns((OrderDto)null);
            var sut = new OrderService(_orderRepository, _mapperMock);
            await Assert.ThrowsAsync<Exception>(async () => await sut.GetOrderByOrderNumber(1));
        }

        [Fact]
        public async Task GetAllOrders_ShouldReturnListOfOrders()
        {
            var orderDtos = new List<OrderDto>
            {
                new OrderDto
                {
                    OrderId = 1,
                    OrderNumber = 1,
                    OrderStatus = "active",
                    UserId = 1
                },
                new OrderDto
                {
                    OrderId = 2,
                    OrderNumber = 2,
                    OrderStatus = "inactive",
                    UserId = 2
                }
            };
            var orders = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    OrderNumber = 1,
                    OrderStatus = "active",
                    UserId = 1
                },
                new Order
                {
                    OrderId = 2,
                    OrderNumber = 2,
                    OrderStatus = "inactive",
                    UserId = 2
                }
            };
            _orderRepository.GetAllOrders().Returns(orderDtos);
            _mapperMock.Map<IEnumerable<Order>>(Arg.Any<IEnumerable<OrderDto>>()).Returns(orders);
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.GetAllOrders();
            Assert.NotNull(result);
            Assert.Equal(orderDtos.Count, result.Count());
        }

        [Fact]
        public async Task GetAllOrders_ShouldReturnEmptyList_WhenNoOrders()
        {
            _orderRepository.GetAllOrders().Returns(new List<OrderDto>());
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.GetAllOrders();
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        [Fact]
        public async Task GetOrdersByUser_ShouldReturnOrders()
        {
            var orderDtos = new List<OrderDto>
            {
                new OrderDto
                {
                    OrderId = 1,
                    OrderNumber = 1,
                    OrderStatus = "active",
                    UserId = 1
                },
                new OrderDto
                {
                    OrderId = 2,
                    OrderNumber = 2,
                    OrderStatus = "inactive",
                    UserId = 1
                }
            };
            var orders = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    OrderNumber = 1,
                    OrderStatus = "active",
                    UserId = 1
                },
                new Order
                {
                    OrderId = 2,
                    OrderNumber = 2,
                    OrderStatus = "inactive",
                    UserId = 1
                }
            };
            _orderRepository.GetOrdersByUser(Arg.Any<int>()).Returns(orderDtos);
            _mapperMock.Map<IEnumerable<Order>>(Arg.Any<IEnumerable<OrderDto>>()).Returns(orders);
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.GetOrdersByUser(1);
            Assert.NotNull(result);
            Assert.Equal(orderDtos.Count, result.Count());
        }
        [Fact]
        public async Task GetOrdersByUser_ShouldReturnEmptyList_WhenNoOrders()
        {
            _orderRepository.GetOrdersByUser(Arg.Any<int>()).Returns(new List<OrderDto>());
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.GetOrdersByUser(1);
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        [Fact]
        public async Task AddOrder_ShouldReturn1_WhenOrderIsAdded()
        {
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 1,
                OrderStatus = "active",
                UserId = 1
            };
            var order = new Order
            {
                OrderStatus = "active",
                OrderNumber = 1,
                OrderId = 1,
                UserId = 1
            };
            _orderRepository.GetOrderByOrderNumber(Arg.Any<int>()).Returns((OrderDto)null);
            _orderRepository.AddOrder(orderDto).Returns(1);
            _mapperMock.Map<OrderDto>(order).Returns(orderDto);
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.AddOrder(order);
            Assert.Equal(1, result);
        }
        [Fact]
        public async Task AddOrder_ShouldThrowException_WhenOrderAlreadyExists()
        {
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 1,
                OrderStatus = "active",
                UserId = 1
            };
            var order = new Order
            {
                OrderStatus = "active",
                OrderNumber = 1,
                OrderId = 1,
                UserId = 1
            };
            _orderRepository.GetOrderByOrderNumber(Arg.Any<int>()).Returns(orderDto);
            _mapperMock.Map<OrderDto>(order).Returns(orderDto);
            var sut = new OrderService(_orderRepository, _mapperMock);
            await Assert.ThrowsAsync<Exception>(async () => await sut.AddOrder(order));
        }
        [Fact]
        public async Task AddOrder_ShouldReturnCorrectNumberOfRows()
        {
            int expectedRows = 1;
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 1,
                OrderStatus = "active",
                UserId = 1
            };
            var order = new Order
            {
                OrderStatus = "active",
                OrderNumber = 1,
                OrderId = 1,
                UserId = 1
            };
            _mapperMock.Map<OrderDto>(order).Returns(orderDto);
            _orderRepository.AddOrder(orderDto).Returns(expectedRows);

            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.AddOrder(order);

            Assert.Equal(expectedRows, result);
        }

        [Fact]
        public async Task DeleteOrder_ShouldReturn1_WhenOrderIsDeleted()
        {
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 1,
                OrderStatus = "active",
                UserId = 1
            };
            var order = new Order
            {
                OrderStatus = "active",
                OrderNumber = 1,
                OrderId = 1,
                UserId = 1
            };
            _orderRepository.GetOrderByOrderNumber(Arg.Any<int>()).Returns(orderDto);
            _orderRepository.DeleteOrder(Arg.Any<int>()).Returns(1);
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.DeleteOrder(order.OrderNumber);
            Assert.Equal(1, result);
        }
        [Fact]
        public async Task DeleteOrder_ShouldThrowException_WhenOrderNotFound()
        {
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 1,
                OrderStatus = "active",
                UserId = 1
            };
            var order = new Order
            {
                OrderStatus = "active",
                OrderNumber = 1,
                OrderId = 1,
                UserId = 1
            };
            _orderRepository.GetOrderByOrderNumber(Arg.Any<int>()).Returns((OrderDto)null);
            _mapperMock.Map<OrderDto>(order).Returns(orderDto);
            var sut = new OrderService(_orderRepository, _mapperMock);           
            await Assert.ThrowsAsync<Exception>(async () => await sut.DeleteOrder(order.OrderNumber));
        }
        [Fact]
        public async Task DeleteOrder_ShouldReturnCorrectNumberOfRows()
        {
            int expectedRows = 1;
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 1,
                OrderStatus = "active",
                UserId = 1
            };
            var order = new Order
            {
                OrderStatus = "active",
                OrderNumber = 1,
                OrderId = 1,
                UserId = 1
            };
            _orderRepository.GetOrderByOrderNumber(Arg.Any<int>()).Returns(orderDto);
            _orderRepository.DeleteOrder(Arg.Any<int>()).Returns(expectedRows);
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.DeleteOrder(order.OrderNumber);
            Assert.Equal(expectedRows, result);
        }

        [Fact]
        public async Task UpdateOrder_ShouldReturn1_WhenOrderIsUpdated()
        {
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 1,
                OrderStatus = "active",
                UserId = 1
            };
            var order = new Order
            {
                OrderStatus = "active",
                OrderNumber = 1,
                OrderId = 1,
                UserId = 1
            };
            _orderRepository.GetOrderById(orderDto.OrderId).Returns(orderDto);
            _mapperMock.Map<OrderDto>(order).Returns(orderDto);
            _orderRepository.UpdateOrder(orderDto).Returns(1);
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.UpdateOrder(order);
            Assert.Equal(1, result);
        }
        [Fact]
        public async Task GetAllOrderDetails_ShouldReturnListOfOrderDetails()
        {
            var orderDetailDtos = new List<OrderDetailDto>
            {
                new OrderDetailDto
                {
                    OrderDetailId = 1,
                    OrderNumber = 1,
                    Price = 10,
                    Quantity = 1,
                    Sku = "SKU1"
                },
                new OrderDetailDto
                {
                    OrderDetailId = 2,
                    OrderNumber = 2,
                    Price = 20,
                    Quantity = 2,
                    Sku = "SKU2"
                }
            };
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    OrderDetailId = 1,
                    OrderNumber = 1,
                    Price = 10,
                    Quantity = 1,
                    Sku = "SKU1"
                },
                new OrderDetail
                {
                    OrderDetailId = 2,
                    OrderNumber = 2,
                    Price = 20,
                    Quantity = 2,
                    Sku = "SKU2"
                }
            };
            _orderRepository.GetAllOrderDetails().Returns(orderDetailDtos);
            _mapperMock.Map<IEnumerable<OrderDetail>>(Arg.Any<IEnumerable<OrderDetailDto>>()).Returns(orderDetails);
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.GetAllOrderDetails();
            Assert.NotNull(result);
            Assert.Equal(orderDetailDtos.Count, result.Count());
        }
        [Fact]
        public async Task GetOrderDetailByOrderNumber_ShouldReturnOrderDetail()
        {
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    OrderDetailId = 1,
                    OrderNumber = 1,
                    Price = 10,
                    Quantity = 1,
                    Sku = "SKU1"
                },
                new OrderDetail
                {
                    OrderDetailId = 2,
                    OrderNumber = 2,
                    Price = 20,
                    Quantity = 2,
                    Sku = "SKU2"
                }
            };
            var orderDetailDto = new List<OrderDetailDto>
            {
                new OrderDetailDto
                {
                    OrderDetailId = 1,
                    OrderNumber = 1,
                    Price = 10,
                    Quantity = 1,
                    Sku = "SKU1"
                },
                new OrderDetailDto
                {
                    OrderDetailId = 2,
                    OrderNumber = 2,
                    Price = 20,
                    Quantity = 2,
                    Sku = "SKU2"
                }
            };
            _orderRepository.GetOrderDetailsByOrderNumber(Arg.Any<int>()).Returns(orderDetailDto);
            _mapperMock.Map<IEnumerable<OrderDetail>>(Arg.Any<IEnumerable<OrderDetailDto>>()).Returns(orderDetails);
            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.GetOrderDetailsByOrderNumber(1);
            Assert.NotNull(result);
            Assert.Equal(orderDetailDto.Count, result.Count());
        }
        [Fact]
        public async Task AddOrderDetail_ShouldReturn1_WhenOrderDetailIsAdded()
        {
            var orderDetail = new OrderDetail {
                OrderDetailId = 1,
                OrderNumber = 1,
                Price = 10,
                Quantity = 1,
                Sku = "SKU1"
            };
            var orderDetailDto = new OrderDetailDto
            {
                OrderDetailId = 1,
                OrderNumber = 1,
                Price = 10,
                Quantity = 1,
                Sku = "SKU1"
            };
            _mapperMock.Map<OrderDetailDto>(orderDetail).Returns(orderDetailDto);
            _orderRepository.AddOrderDetail(orderDetailDto).Returns(1);

            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.AddOrderDetail(orderDetail);
            Assert.Equal(1, result);
        }
        [Fact]
        public async Task UpdateOrder_ShouldReturnOne_WhenOrderIsUpdated()
        {
            var order = new Order
            {
                OrderId = 1,
                OrderNumber = 200,
                OrderStatus = "inactive",
                UserId = 2
            };
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 200,
                OrderStatus = "inactive",
                UserId = 2
            };
            _orderRepository.GetOrderById(orderDto.OrderId).Returns(orderDto);
            _mapperMock.Map<OrderDto>(order).Returns(orderDto);
            _orderRepository.UpdateOrder(orderDto).Returns(1);

            var sut = new OrderService(_orderRepository, _mapperMock);
            var result = await sut.UpdateOrder(order);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteOrder_ShouldReturnOne_WhenOrderIsDeleted()
        {
            var order = new Order
            {
                OrderId = 1,
                OrderNumber = 200,
                OrderStatus = "inactive",
                UserId = 2
            };
            var orderDto = new OrderDto
            {
                OrderId = 1,
                OrderNumber = 200,
                OrderStatus = "inactive",
                UserId = 2
            };
            _orderRepository.GetOrderByOrderNumber(orderDto.OrderNumber).Returns(orderDto);
            _mapperMock.Map<OrderDto>(order).Returns(orderDto);
            _orderRepository.DeleteOrder(orderDto.OrderNumber).Returns(1);
            var sut = new OrderService(_orderRepository, _mapperMock);

            var result = await sut.DeleteOrder(order.OrderNumber);
            Assert.Equal(1, result);
        }

    };
}
    

