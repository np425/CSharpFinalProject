using ApiData.Models;
using BlazorFrontend.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorFrontend.Services;

public class SubjectMemberService : ApiServiceBase
{
    private readonly UserService _userService;

    public SubjectMemberService(NavigationManager navigationManager, HttpClient httpClient, UserService userService) :
        base(navigationManager,
            httpClient)
    {
        _userService = userService;
    }

    public List<Teacher> Teachers { get; private set; } = new();
    public List<Student> Students { get; private set; } = new();
    public List<UserDto> Users { get; private set; } = new();

    public async Task LoadMembersAsync(int subjectId)
    {
        Teachers = new();
        Students = new();

        var members = await HttpClient.GetFromJsonAsync<List<SubjectMemberDto>>(NavigationManager.BaseUri + $"api/subjects/{subjectId}/members") ??
                      new();
        
        await _userService.LoadUsersAsync();
        Users = _userService.Users;

        var query = from member in members
            join user in Users on member.UserId equals user.Id
            select (user, member);

        foreach (var (user, member) in query)
        {
            switch (member.Role)
            {
                case SubjectMemberRole.Student:
                    Students.Add(new Student
                    {
                        User = user,
                        MemberId = member.MemberId,
                        SubjectId = subjectId
                    });
                    break;
                case SubjectMemberRole.Teacher:
                    Teachers.Add(new Teacher
                    {
                        User = user,
                        MemberId = member.MemberId,
                        SubjectId = subjectId
                    });
                    break;
            }
        }
    }

    public async Task CreateMemberAsync(Member item)
    {
        var dtoItem = new SubjectMemberDto
        {
            MemberId = item.MemberId,
            SubjectId = item.SubjectId,
            UserId = item.User.Id,
        };
        
        if (item.GetType() == typeof(Student))
        {
            Students.Add((Student)item);
            dtoItem.Role = SubjectMemberRole.Student;
        }
        else if (item.GetType() == typeof(Teacher))
        {
            Teachers.Add((Teacher)item);
            dtoItem.Role = SubjectMemberRole.Teacher;
        }

        await HttpClient.PostAsJsonAsync(NavigationManager.BaseUri + $"api/subjects/{item.SubjectId}/members", dtoItem);
    }

    public async Task UpdateMemberAsync(Member item)
    {
        var dtoItem = new SubjectMemberDto
        {
            MemberId = item.MemberId,
            SubjectId = item.SubjectId,
            UserId = item.User.Id,
        };
        
        if (item.GetType() == typeof(Student))
        {
            dtoItem.Role = SubjectMemberRole.Student;
        }
        else if (item.GetType() == typeof(Teacher))
        {
            dtoItem.Role = SubjectMemberRole.Teacher;
        }
        
        await HttpClient.PutAsJsonAsync(NavigationManager.BaseUri + $"api/subjects/{item.SubjectId}/members/{item.MemberId}", dtoItem);
    }

    public async Task DeleteMemberAsync(Member item)
    {
        if (item.GetType() == typeof(Student))
        {
            Students.Remove((Student)item);
        }
        else if (item.GetType() == typeof(Teacher))
        {
            Teachers.Remove((Teacher)item);
        }

        await HttpClient.DeleteAsync(NavigationManager.BaseUri + $"api/subjects/{item.SubjectId}/members/{item.MemberId}");
    }
}
