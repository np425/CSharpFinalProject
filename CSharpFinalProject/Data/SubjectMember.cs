using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpFinalProject.Data;

[Table("SubjectsMembers")]
public class SubjectMember
{
    public int UserId { get; set; }
    public int SubjectId { get; set; }
    public SubjectMemberRole Role { get; set; }

    public User User { get; set; } = null!;
    
    public Subject Subject { get; set; } = null!;
}
