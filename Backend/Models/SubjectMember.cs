using System.ComponentModel.DataAnnotations.Schema;
using ApiData.Models;

namespace Backend.Models;

[Table("SubjectsMembers")]
public class SubjectMember
{
    public int UserId { get; set; }
    public int SubjectId { get; set; }
    public SubjectMemberRole Role { get; set; }
    public int Id { get; set; }
}
