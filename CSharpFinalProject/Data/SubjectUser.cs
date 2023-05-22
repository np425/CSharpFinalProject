using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpFinalProject.Data;

[Table("SubjectsMembers")]
public class SubjectUser
{
    public int UserId { get; set; }
    public int SubjectId { get; set; }
    public SubjectMemberRole Role { get; set; }
    public int Id { get; set; }
}
