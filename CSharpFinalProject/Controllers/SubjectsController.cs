using CSharpFinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Radzen;

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
    public async Task<ActionResult?> PostSubject([FromBody] Subject subject)
    {
        _db.Subjects.Add(subject);
        await _db.SaveChangesAsync();

        return null;
    }


    [HttpPut("{subjectId:int}")]
    public async Task<ActionResult?> PutSubject(int subjectId, [FromBody] Subject subject)
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

    #region Subject Members

    [HttpGet("{subjectId:int}/students")]
    public async Task<ActionResult<List<Student>>> GetStudents(int subjectId)
    {
        Console.WriteLine(await _db.SubjectsMembers.CountAsync());

        var query = from member in _db.SubjectsMembers
            join user in _db.Users on member.UserId equals user.Id
            where member.SubjectId == subjectId && member.Role == SubjectMemberRole.Student
            select new Student
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                MemberId = member.Id
            };

        return await query.ToListAsync();
    }

    [HttpGet("{subjectId:int}/teachers")]
    public async Task<ActionResult<List<Teacher>>> GetTeachers(int subjectId)
    {
        var query = from member in _db.SubjectsMembers
            join user in _db.Users on member.UserId equals user.Id
            where member.SubjectId == subjectId && member.Role == SubjectMemberRole.Teacher
            select new Teacher
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                MemberId = member.Id
            };

        return await query.ToListAsync();
    }

    [HttpPost("{subjectId:int}/members")]
    public async Task<ActionResult?> PostMember(int subjectId, [FromBody] SubjectMember member)
    {
        var newItem = new SubjectUser
        {
            SubjectId = subjectId,
            Role = member.MemberRole,
            UserId = member.Id
        };

        _db.SubjectsMembers.Add(newItem);
        await _db.SaveChangesAsync();

        return null;
    }

    [HttpPut("{subjectId:int}/members/{memberId:int}")]
    public async Task<ActionResult?> PutMember(int subjectId, int memberId, [FromBody] SubjectMember member)
    {
        if (memberId != member.MemberId)
        {
            return null;
        }

        var item = await _db.SubjectsMembers.FindAsync(memberId);
        if (item == null) return null;

        item.UserId = member.Id;
        item.Role = member.MemberRole;
        item.SubjectId = subjectId;

        _db.Entry(item).State = EntityState.Modified;

        await _db.SaveChangesAsync();

        return null;
    }

    [HttpDelete("{subjectId:int}/members/{memberId:int}")]
    public async Task<ActionResult?> DeleteMember(int subjectId, int memberId)
    {
        var item = await _db.SubjectsMembers.FindAsync(memberId);
        if (item == null)
        {
            return null;
        }

        _db.SubjectsMembers.Remove(item);
        await _db.SaveChangesAsync();

        return null;
    }

    #endregion
}