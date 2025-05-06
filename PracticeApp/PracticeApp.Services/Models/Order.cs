

namespace PracticeApp.Services.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderStatus { get; set; }
    }
}
