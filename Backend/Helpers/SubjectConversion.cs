using ApiData.Models;
using Backend.Models;

namespace Backend.Helpers;

public class SubjectConversion: IDtoConversion<Subject, SubjectDto>
{
    public Subject ToInternal(SubjectDto subjectDto)
    {
        var subject = new Subject();
        LoadDtoData(subject, subjectDto);
        return subject;
    }

    public void LoadDtoData(Subject internalData, SubjectDto dtoData)
    {
        internalData.Id = dtoData.Id;
        internalData.Description = dtoData.Description;
        internalData.Name = dtoData.Name;
    }

    public SubjectDto ToDto(Subject subject)
    {
        return new SubjectDto
        {
            Description = subject.Description,
            Name = subject.Name ?? "",
            Id = subject.Id
        };
    }
}
