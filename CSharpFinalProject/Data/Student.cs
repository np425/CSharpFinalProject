namespace CSharpFinalProject.Data;

public class Student: User
{
    public Student(string username, string email, int id) : base(username, email, id)
    {
    }
    
    public Subject[] Subjects;
    public Grade[] Grades;
}
