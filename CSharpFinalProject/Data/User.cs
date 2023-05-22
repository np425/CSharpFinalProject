using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpFinalProject.Data;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
}
