using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApp.Domain.Models;
[Table("OrderDetails")]
public partial class OrderDetailDto
{
    [Key]
    public int OrderDetailId { get; set; }
    //[ForeignKey("Order")]
    public int OrderNumber { get; set; }

    public string Sku { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

}