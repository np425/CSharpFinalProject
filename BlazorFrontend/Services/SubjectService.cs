using ApiData.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorFrontend.Services;

public class SubjectService: ApiServiceBase
{
    public SubjectService(NavigationManager navigationManager, HttpClient httpClient) : base(navigationManager, httpClient)
    {
    }

    public List<SubjectDto> Subjects { get; private set; } = new();
    
    public async Task LoadSubjectsAsync()
    {
        Subjects = await HttpClient.GetFromJsonAsync<List<SubjectDto>>(NavigationManager.BaseUri + "api/subjects") ?? new();
    }

    public async Task CreateSubjectAsync(SubjectDto item)
    {
        Subjects.Add(item);
        await HttpClient.PostAsJsonAsync(NavigationManager.BaseUri + "api/subjects/", item);
    }

    public async Task UpdateSubjectAsync(SubjectDto item)
    {
        await HttpClient.PutAsJsonAsync(NavigationManager.BaseUri + $"api/subjects/{item.Id}", item);
    }

    public async Task DeleteSubjectAsync(SubjectDto item)
    {
        Subjects.Remove(item);
        await HttpClient.DeleteAsync(NavigationManager.BaseUri + $"api/subjects/{item.Id}");
    }
}
