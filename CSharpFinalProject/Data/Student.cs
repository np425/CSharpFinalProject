namespace CSharpFinalProject.Data;

public class Student: SubjectMember
{
    public Student()
    {
        MemberRole = SubjectMemberRole.Student;
    }
}