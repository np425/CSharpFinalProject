using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpFinalProject.Data;

[Table("SubjectsUsersGrades")]
public class SubjectUserGrade
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubjectId { get; set; }
    public double Grade { get; set; }
    public string? Note { get; set; }
    public DateTime Date { get; set; }
}
