using ApiData.Models;
using Backend.Context;
using Backend.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/subjects")]
[ApiController]
public class SubjectsController
{
    private readonly OnlineSchoolStoreContext _db;
    private readonly SubjectConversion _subjectConversion;

    public SubjectsController(OnlineSchoolStoreContext db)
    {
        _db = db;
        _subjectConversion = new SubjectConversion();
    }

    [HttpGet]
    public async Task<ActionResult<List<SubjectDto>>> GetSubjects()
    {
        return await _db.Subjects.Select(subject => _subjectConversion.ToDto(subject)).ToListAsync();
    }


    [HttpPost]
    public async Task<ActionResult?> PostSubject([FromBody] SubjectDto subjectDto)
    {
        _db.Subjects.Add(_subjectConversion.ToInternal(subjectDto));
        await _db.SaveChangesAsync();

        return null;
    }


    [HttpPut("{subjectId:int}")]
    public async Task<ActionResult?> PutSubject(int subjectId, [FromBody] SubjectDto subjectDto)
    {
        if (subjectId != subjectDto.Id)
        {
            return null;
        }

        var item = await _db.Subjects.FindAsync(subjectId);
        if (item == null)
        {
            return null;
        }

        _subjectConversion.LoadDtoData(item, subjectDto);
        _db.Entry(item).State = EntityState.Modified;

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
}
