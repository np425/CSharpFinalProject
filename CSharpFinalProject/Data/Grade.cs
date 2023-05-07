namespace CSharpFinalProject.Data;

public class Grade
{
    public int Id { get; set; }
    public int ClassID { get; set; }
    public int StudentId { get; set; }
    
    public Subject Subject { get; set; }
    public Student Student { get; set; }
}
