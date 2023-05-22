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
    
    [HttpPost]
    public async Task<ActionResult?> PostUser([FromBody]User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return null;
    }

    [HttpPut("{userId:int}")]
    public async Task<ActionResult?> PutUser(int userId, [FromBody]User user)
    {
        if (userId != user.Id)
        {
            return null;
        }

        _db.Entry(user).State = EntityState.Modified;

        await _db.SaveChangesAsync();
        
        return null;
    }

    [HttpDelete("{userId:int}")]
    public async Task<ActionResult?> DeleteUser(int userId)
    {
        var item = await _db.Users.FindAsync(userId);
        if (item == null)
        {
            return null;
        }

        _db.Users.Remove(item);
        await _db.SaveChangesAsync();

        return null;
    }
}
