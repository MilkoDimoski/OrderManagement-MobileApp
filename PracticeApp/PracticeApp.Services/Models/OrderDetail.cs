

namespace PracticeApp.Services.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderNumber { get; set; }

        public string Sku { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
