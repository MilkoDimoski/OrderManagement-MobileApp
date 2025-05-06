using PracticeApp.Domain.Models;

namespace PracticeApp.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<OrderDto>>GetAllOrders();
        public Task<OrderDto> GetOrderByOrderNumber(int orderNumber);
        public Task<OrderDto> GetOrderById(int id);
        public Task<IEnumerable <OrderDto>> GetOrdersByUser(int userId);
        public Task<int> AddOrder(OrderDto order);
        public Task<int> UpdateOrder(OrderDto order);
        public Task<int> DeleteOrder(int orderNumber);


        public Task<IEnumerable<OrderDetailDto>> GetAllOrderDetails();
        public Task<OrderDetailDto> GetOrderDetailById(int orderId);
        public Task<IEnumerable< OrderDetailDto>> GetOrderDetailsByOrderNumber(int orderNumber);
        public Task<int> AddOrderDetail(OrderDetailDto orderDetail);
        public Task<int> UpdateOrderDetail(OrderDetailDto orderDetail);
        public Task<int> DeleteOrderDetail(int orderNumber);

    }
}
