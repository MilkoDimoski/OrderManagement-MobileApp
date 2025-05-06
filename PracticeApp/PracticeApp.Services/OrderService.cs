using AutoMapper;
using PracticeApp.Domain.Models;
using PracticeApp.Repository.Interfaces;
using PracticeApp.Services.Interfaces;
using PracticeApp.Services.Models;

namespace PracticeApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository,IMapper mapper)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task<int> AddOrder(Order order)
        {
            var orderDto = _mapper.Map<OrderDto>(order);
            var existingOrder = await _orderRepository.GetOrderByOrderNumber(order.OrderNumber);
            if (existingOrder != null)
            {
                throw new Exception("Order already exists");
            }
            return await _orderRepository.AddOrder(orderDto);
        }

        public async Task<int> AddOrderDetail(OrderDetail orderDetail)
        {
            var orderDetailDto = _mapper.Map<OrderDetailDto>(orderDetail);
            var existingOrderDetail = await _orderRepository.GetOrderDetailById(orderDetail.OrderDetailId);
            if (existingOrderDetail != null)
            {
                throw new Exception("Order detail already exists");
            }
            return await _orderRepository.AddOrderDetail(orderDetailDto);
        }

        public async Task<int> DeleteOrder(int orderNumber)
        {
            var existingOrder =await _orderRepository.GetOrderByOrderNumber(orderNumber);
            if (existingOrder == null)
            {
                throw new Exception("Order not found");
            }
            return await _orderRepository.DeleteOrder(orderNumber);
        }

        public Task<int> DeleteOrderDetail(int orderNumber)
        {
            var existingOrderDetail = _orderRepository.GetOrderDetailById(orderNumber);
            if (existingOrderDetail == null)
            {
                throw new Exception("Order detail not found");
            }
            return _orderRepository.DeleteOrderDetail(orderNumber);
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetails()
        {
            var orderDetailDtos = await _orderRepository.GetAllOrderDetails();
            if (orderDetailDtos == null)
            {
                throw new Exception("Order details not found");
            }
            var orderDetails = _mapper.Map<IEnumerable<OrderDetail>>(orderDetailDtos);
            return orderDetails;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orderDtos = await _orderRepository.GetAllOrders();
            var orders = _mapper.Map<IEnumerable<Order>>(orderDtos);
            return orders;
        }

        public async Task<Order> GetOrderById(int id)
        {
            var orderDto =await _orderRepository.GetOrderById(id);
            if (orderDto == null)
            {
                throw new Exception("Order not found");
            }
            var order = _mapper.Map<Order>(orderDto);
            return order;
        }

        public async Task<Order> GetOrderByOrderNumber(int orderNumber)
        {
            var orderDto = await _orderRepository.GetOrderByOrderNumber(orderNumber);
            if (orderDto == null)
            {
                throw new Exception("Order not found");
            }
            var order = _mapper.Map<Order>(orderDto);
            return order;
        }

        public async Task<OrderDetail> GetOrderDetailById(int orderId)
        {
            var orderDetailDto = await _orderRepository.GetOrderDetailById(orderId);
            if (orderDetailDto == null)
            {
                throw new Exception("Order detail not found");
            }
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);
            return orderDetail;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderNumber(int orderNumber)
        {
            var orderDetailDtos = await _orderRepository.GetOrderDetailsByOrderNumber(orderNumber);
            if (orderDetailDtos == null)
            {
                throw new Exception("Order details not found");
            }
            var orderDetails = _mapper.Map<IEnumerable<OrderDetail>>(orderDetailDtos);
            return orderDetails;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUser(int userId)
        {
            var orderDtos = await _orderRepository.GetOrdersByUser(userId);
            var orders = _mapper.Map<IEnumerable<Order>>(orderDtos);
            return orders;
        }

        public async Task<int> UpdateOrder(Order order)
        {
            var orderDto = _mapper.Map<OrderDto>(order);
            var existingOrder = await _orderRepository.GetOrderById(orderDto.OrderId);
            if (existingOrder == null)
            {
                throw new Exception("Order not found");
            }
            return await _orderRepository.UpdateOrder(orderDto);
        }

        public async Task<int> UpdateOrderDetail(OrderDetail orderDetail)
        {
            var orderDetailDto = _mapper.Map<OrderDetailDto>(orderDetail);
            var existingOrderDetail = await _orderRepository.GetOrderDetailById(orderDetailDto.OrderDetailId);
            if (existingOrderDetail == null)
            {
                throw new Exception("Order detail not found");
            }
            return await _orderRepository.UpdateOrderDetail(orderDetailDto);
        }
    }
}
