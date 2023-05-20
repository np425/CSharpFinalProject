using CSharpFinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpFinalProject.Controllers;

[Route("api/subjects")]
[ApiController]
public class SubjectsController
{
    private readonly GradeSystemStoreContext _db;

    public SubjectsController(GradeSystemStoreContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Subject>>> GetSubjects()
    {
        return await _db.Subjects.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Subject?>> GetSubject(int id)
    {
        return await _db.Subjects.FindAsync(id);
    }

    [HttpGet("{subjectId:int}/members")]
    public async Task<ActionResult<List<SubjectMember>>> GetMembers(int subjectId)
    {
        var subjectsMembers = _db.SubjectsMembers;
        
        var foundMembers = await (from member in subjectsMembers
                where member.SubjectId == subjectId
                select member)
            .Include(member => member.User)
            .ToListAsync();
        
        foundMembers.ForEach(m => Console.WriteLine(m.Role));
        
        return foundMembers;
    }
}
