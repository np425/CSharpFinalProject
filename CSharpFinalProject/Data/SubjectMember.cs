namespace CSharpFinalProject.Data;

public class SubjectMember: User
{
    public SubjectMemberRole MemberRole { get; set; }
    public int MemberId { get; set; }
}