namespace CSharpFinalProject.Data;

public class UserWithSubjectRole: User
{
    public SubjectMemberRole RoleInSubject { get; set; }
}