using ApiData.Models;

namespace BlazorFrontend.Data;

public class Member
{
    public UserDto User { get; set; }
    public int SubjectId { get; set; }
    public int MemberId { get; set; }
}
