using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApp.Domain.Models;
[Table("Users")]
public partial class UserDto
{
    [Key]
    public int UserId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

}