using ApiData.Models;
using Backend.Context;
using Backend.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/subjects/{subjectId:int}/members")]
[ApiController]
public class MembersController
{
    private readonly OnlineSchoolStoreContext _db;
    private readonly SubjectMemberConversion _memberConversion;

    public MembersController(OnlineSchoolStoreContext db)
    {
        _db = db;
        _memberConversion = new SubjectMemberConversion();
    }
    
    [HttpGet]
    public async Task<ActionResult<List<SubjectMemberDto>>> GetStudents(int subjectId)
    {
        var query = from member in _db.SubjectsMembers
            join user in _db.Users on member.UserId equals user.Id
            where member.SubjectId == subjectId 
            select _memberConversion.ToDto(member);
    
        return await query.ToListAsync();
    }
    
    [HttpPost]
    public async Task<ActionResult?> PostMember(int subjectId, [FromBody] SubjectMemberDto memberDto)
    {
        var newItem = _memberConversion.ToInternal(memberDto);
    
        _db.SubjectsMembers.Add(newItem);
        await _db.SaveChangesAsync();
    
        return null;
    }
    
    [HttpPut("{memberId:int}")]
    public async Task<ActionResult?> PutMember(int subjectId, int memberId, [FromBody] SubjectMemberDto memberDto)
    {
        if (memberId != memberDto.MemberId)
        {
            return null;
        }
    
        var item = await _db.SubjectsMembers.FindAsync(memberId);
        if (item == null) return null;
    
        _memberConversion.LoadDtoData(item, memberDto);
        _db.Entry(item).State = EntityState.Modified;
    
        await _db.SaveChangesAsync();
    
        return null;
    }
    
    [HttpDelete("{memberId:int}")]
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
}
