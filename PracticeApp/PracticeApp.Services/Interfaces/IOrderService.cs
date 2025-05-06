using PracticeApp.Services.Models;

namespace PracticeApp.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetAllOrders();
        public Task<Order> GetOrderByOrderNumber(int orderNumber);
        public Task<Order> GetOrderById(int id);
        public Task<IEnumerable<Order>> GetOrdersByUser(int userId);
        public Task<int> AddOrder(Order order);
        public Task<int> UpdateOrder(Order order);
        public Task<int> DeleteOrder(int orderNumber);


        public Task<IEnumerable<OrderDetail>> GetAllOrderDetails();
        public Task<OrderDetail> GetOrderDetailById(int orderId);
        public Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderNumber(int orderNumber);
        public Task<int> AddOrderDetail(OrderDetail orderDetail);
        public Task<int> UpdateOrderDetail(OrderDetail orderDetail);
        public Task<int> DeleteOrderDetail(int orderNumber);
    }
}
