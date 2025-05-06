
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PracticeApp.Domain.Models;
using PracticeApp.Repository.Interfaces;

namespace PracticeApp.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? String.Empty;
        }
        public async Task<int> AddOrder(OrderDto order)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Orders(UserId,OrderNumber,OrderStatus,OrderDate)
                            VALUES(@UserId,@OrderNumber,@OrderStatus,@OrderDate)";
                return await connection.ExecuteAsync(sql, order);
            }
        }

        public async Task<int> AddOrderDetail(OrderDetailDto orderDetail)
        {
            using (var connection = new SqlConnection(_connectionString)) 
            {
                var sql = @"INSERT INTO OrderDetails(OrderNumber,Sku,Price,Quantity)
                            VALUES(@OrderNumber,@Sku,@Price,@Quantity)";
                return await connection.ExecuteAsync(sql, orderDetail);
            }
        }
        public async Task<OrderDto> GetOrderById(int orderId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Orders
                    WHERE OrderId=@OrderId";
                var order = connection.QueryFirstOrDefaultAsync<OrderDto>(sql, new { OrderId = orderId });
                return await order;
            }
        }

        public async Task<int> DeleteOrder(int orderNumber)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE FROM Orders
                            WHERE OrderNumber=@OrderNumber";
                return await connection.ExecuteAsync(sql, new { OrderNumber = orderNumber });
            }
        }

        public async Task<int> DeleteOrderDetail(int id)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE FROM OrderDetails
                            WHERE OrderDetailId=@OrderDetailId";
                return await connection.ExecuteAsync(sql, new { OrderDetailId = id });
            }
        }

        public async Task<IEnumerable<OrderDetailDto>> GetAllOrderDetails()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM OrderDetails";
                var orderDetails = await connection.QueryAsync<OrderDetailDto>(sql);
                return orderDetails;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Orders";
                var orders = await connection.QueryAsync<OrderDto>(sql);
                return orders;
            }
        }

        public async Task<OrderDto> GetOrderByOrderNumber(int orderNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Orders
                    WHERE OrderNumber=@OrderNumber";
                var order= connection.QueryFirstOrDefaultAsync<OrderDto>(sql, new {OrderNumber= orderNumber });
                return await order;
            }
        }

        public async Task<IEnumerable< OrderDto>> GetOrdersByUser(int userId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Orders
                    WHERE UserId=@UserId";
                var orders = connection.QueryAsync<OrderDto>(sql, new { UserId = userId });
                return await orders;
            }
        }

        public async Task<OrderDetailDto> GetOrderDetailById(int orderId)
        {
           using( var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM OrderDetails
                    WHERE OrderDetailId=@OrderDetailId";
                var orderDetail = connection.QueryFirstOrDefaultAsync<OrderDetailDto>(sql, new { OrderDetailId = orderId });
                return await orderDetail;
            }
        }

        public async Task<IEnumerable<OrderDetailDto>> GetOrderDetailsByOrderNumber(int orderNumber)
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM OrderDetails
                    WHERE OrderNumber=@OrderNumber";
                var orderDetails =await connection.QueryAsync<OrderDetailDto>(sql, new { OrderNumber = orderNumber });
                return orderDetails;
            }
        }

        public async Task<int> UpdateOrder(OrderDto order)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Orders
                            SET UserId=@UserId,
                            OrderStatus=@OrderStatus,
                            OrderDate=@OrderDate
                            WHERE OrderNumber=@OrderNumber";
                return await connection.ExecuteAsync(sql, order);
            }
        }

        public async Task<int> UpdateOrderDetail(OrderDetailDto orderDetail)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE OrderDetails
                            SET Sku=@Sku,
                            Price=@Price,
                            Quantity=@Quantity
                            WHERE OrderDetailId=@OrderDetailId";
                return await connection.ExecuteAsync(sql, orderDetail);
            }
        }
    }
}
