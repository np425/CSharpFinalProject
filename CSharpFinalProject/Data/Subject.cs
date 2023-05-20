namespace CSharpFinalProject.Data;

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
    
    public List<SubjectMember> Members { get; set; }

    // public int Id { get; set; }
    // public int TeacherId { get; set; }
    // public int ClassId { get; set; }
    // public Teacher Teacher { get; set; }
    // public Student[] Students { get; set; }
}