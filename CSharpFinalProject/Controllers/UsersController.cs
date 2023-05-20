using CSharpFinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpFinalProject.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController
{
    private readonly GradeSystemStoreContext _db;

    public UsersController(GradeSystemStoreContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var users = _db.Users;
        var subjectsMembers = _db.SubjectsMembers;

        return await users.ToListAsync();
    }

    [HttpGet("{userId:int}")]
    public async Task<ActionResult<User>> GetUser(int userId)
    {
        var user = await _db.Users.FindAsync(userId);

        return user;
    }

    [HttpGet("{userId:int}/subjects/")]
    public async Task<ActionResult<List<SubjectWithMemberRole>>> GetSubjects(int userId)
    {
        var members = _db.SubjectsMembers;
        var subjects = _db.Subjects;

        var result = await (from member in members
                join subject in subjects on member.SubjectId equals subject.Id
                where member.UserId == userId
                select new SubjectWithMemberRole
                {
                    Subject = subject,
                    MemberRole = member.Role
                })
            .ToListAsync();

        return result;
    }
}
