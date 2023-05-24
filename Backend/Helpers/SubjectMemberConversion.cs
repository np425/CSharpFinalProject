using ApiData.Models;
using Backend.Models;

namespace Backend.Helpers;

public class SubjectMemberConversion: IDtoConversion<SubjectMember, SubjectMemberDto>
{
    public SubjectMemberDto ToDto(SubjectMember member)
    {
        return new SubjectMemberDto
        {
            Role = member.Role,
            MemberId = member.Id,
            SubjectId = member.SubjectId,
            UserId = member.UserId
        };
    }

    public SubjectMember ToInternal(SubjectMemberDto memberDto)
    {
        return new SubjectMember
        {
            Id = memberDto.MemberId,
            SubjectId = memberDto.SubjectId,
            UserId = memberDto.UserId,
            Role = memberDto.Role
        };
    }

    public void LoadDtoData(SubjectMember internalData, SubjectMemberDto dtoData)
    {
        internalData.Id = dtoData.MemberId;
        internalData.SubjectId = dtoData.SubjectId;
        internalData.UserId = dtoData.UserId;
        internalData.Role = dtoData.Role;
    }
}
