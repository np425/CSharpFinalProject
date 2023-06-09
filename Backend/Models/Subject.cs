using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Subjects")]
public class Subject
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    public string? Description { get; set; }
}
