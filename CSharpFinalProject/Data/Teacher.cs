namespace CSharpFinalProject.Data;

public class Teacher: User
{
    public Teacher(string username, string email, int id) : base(username, email, id)
    {
    }

    public Subject[] Subjects;
}
