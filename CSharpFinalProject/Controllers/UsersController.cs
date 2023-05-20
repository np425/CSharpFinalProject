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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _db.Users.FindAsync(id);

        return user;
    }

    [HttpGet("{id:int}/subjects")]
    public async Task<ActionResult<List<SubjectMember>>> GetSubjects(int id)
    {
        var members = _db.SubjectsMembers;
        return await (from member in members select member).ToListAsync();
    }
}
