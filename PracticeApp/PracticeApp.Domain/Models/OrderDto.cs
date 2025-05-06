using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApp.Domain.Models;
[Table("Orders")]
public partial class OrderDto
{
    [Key]
    public int OrderId { get; set; }
    [ForeignKey("User")]

    public int UserId { get; set; }

    public int OrderNumber { get; set; }

    public DateTime OrderDate { get; set; }

    public string OrderStatus { get; set; }

}