using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpFinalProject.Data;

[Table("SubjectsUsers")]
public class SubjectUser
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubjectId { get; set; }
    public SubjectUserRole Role { get; set; }

    public User User { get; set; } = null!;

    public Subject Subject { get; set; } = null!;
}
