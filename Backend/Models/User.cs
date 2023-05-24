using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
}
