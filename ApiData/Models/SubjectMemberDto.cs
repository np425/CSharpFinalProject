namespace ApiData.Models;

public class SubjectMemberDto
{
    public SubjectMemberRole Role { get; set; }
    public int MemberId { get; set; }
    public int UserId { get; set; }
    public int SubjectId { get; set; }
}
