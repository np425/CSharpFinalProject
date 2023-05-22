using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpFinalProject.Data;

[Table("Subjects")]
public class Subject
{
    // public Subject(int id, int classId)
    // {
    //     Id = id;
    //     ClassId = classId;
    // }
    
    public int Id { get; set; }
    
    public string? Name { get; set; }
    public string? Description { get; set; }

    // public int Id { get; set; }
    // public int TeacherId { get; set; }
    // public int ClassId { get; set; }
    // public Teacher Teacher { get; set; }
    // public Student[] Students { get; set; }
}
