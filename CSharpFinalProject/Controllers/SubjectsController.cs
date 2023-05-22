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
    public async Task<ActionResult<List<Subject>>> GetSubjects([FromQuery] DataQueryApiArgs args)
    {
        var query = _db.Subjects.AsQueryable();

        // if (args.Skip != null)
        // {
        //     query = query.Skip(args.Skip.Value);
        // }
        //
        // if (args.Top != null)
        // {
        //     query = query.Take(args.Top.Value);
        // }
        
        return await query.ToListAsync();
    }


    [HttpGet("{subjectId:int}")]
    public async Task<ActionResult<Subject?>> GetSubject(int subjectId)
    {
        return await _db.Subjects.FindAsync(subjectId);
    }

    [HttpPost]
    public async Task<ActionResult?> PostSubject([FromBody]Subject subject)
    {
        _db.Subjects.Add(subject);
        await _db.SaveChangesAsync();

        return null;
    }

    [HttpPut("{subjectId:int}")]
    public async Task<ActionResult?> PutSubject(int subjectId, [FromBody]Subject subject)
    {
        if (subjectId != subject.Id)
        {
            return null;
        }

        _db.Entry(subject).State = EntityState.Modified;

        await _db.SaveChangesAsync();
        
        return null;
    }

    [HttpDelete("{subjectId:int}")]
    public async Task<ActionResult?> DeleteSubject(int subjectId)
    {
        var item = await _db.Subjects.FindAsync(subjectId);
        if (item == null)
        {
            return null;
        }

        _db.Subjects.Remove(item);
        await _db.SaveChangesAsync();

        return null;
    }

    [HttpGet("{subjectId:int}/members")]
    public async Task<ActionResult<List<SubjectUser>>> GetMembers(int subjectId)
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

    [HttpGet("{subjectId:int}/grades/")]
    public async Task<ActionResult<List<SubjectUserGrade>>> GetGrade(int subjectId, int grade)
    {
        return await _db.StudentGrades.Where(g => g.SubjectId == subjectId).ToListAsync();
    }
}
