using Microsoft.AspNetCore.Mvc;
using PracticeApp.Services.Interfaces;
using PracticeApp.Services.Models;

namespace PracticeApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController:ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUser(userId);
            return Ok(orders);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            var orderId = await _orderService.AddOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, order);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            var existingOrder = await _orderService.GetOrderById(order.OrderId);
            if (existingOrder == null)
            {
                return NotFound();
            }
            await _orderService.UpdateOrder(order);
            return Ok(order);
        }
        [HttpDelete("{orderNumber}")]
        public async Task<IActionResult> DeleteOrder(int orderNumber)
        {
            var existingOrder = await _orderService.GetOrderByOrderNumber(orderNumber);
            if (existingOrder == null)
            {
                return NotFound();
            }
            await _orderService.DeleteOrder(orderNumber);
            return Ok(existingOrder);
        }
        [HttpGet("details")]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var orderDetails = await _orderService.GetAllOrderDetails();
            return Ok(orderDetails);
        }
        [HttpGet("details/{orderId}")]
        public async Task<IActionResult> GetOrderDetailById(int orderId)
        {
            var orderDetail = await _orderService.GetOrderDetailById(orderId);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }
        [HttpGet("details/order/{orderNumber}")]
        public async Task<IActionResult> GetOrderDetailsByOrderNumber(int orderNumber)
        {
            var orderDetails = await _orderService.GetOrderDetailsByOrderNumber(orderNumber);
            return Ok(orderDetails);
        }
        [HttpPost("details")]
        public async Task<IActionResult> AddOrderDetail(OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                return BadRequest();
            }
            var orderDetailId = await _orderService.AddOrderDetail(orderDetail);
            return CreatedAtAction(nameof(GetOrderDetailsByOrderNumber),new { orderNumber = orderDetail.OrderNumber },orderDetail );
        }
        [HttpPut("details/{id}")]
        public async Task<IActionResult> UpdateOrderDetail(OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                return BadRequest();
            }
            var existingOrderDetail = await _orderService.GetOrderDetailById(orderDetail.OrderDetailId);
            if (existingOrderDetail == null)
            {
                return NotFound();
            }
            await _orderService.UpdateOrderDetail(orderDetail);
            return Ok(orderDetail);
        }
        [HttpDelete("details/{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var existingOrderDetail = await _orderService.GetOrderDetailById(id);
            if (existingOrderDetail == null)
            {
                return NotFound();
            }
            await _orderService.DeleteOrderDetail(id);
            return Ok(existingOrderDetail);
        }
    }
}
