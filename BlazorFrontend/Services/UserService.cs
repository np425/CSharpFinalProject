using ApiData.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorFrontend.Services;

public class UserService : ApiServiceBase
{
    public List<UserDto> Users { get; private set; } = new();

    public UserService(NavigationManager navigationManager, HttpClient httpClient) :
        base(navigationManager, httpClient)
    {
    }

    public async Task LoadUsersAsync()
    {
        Users = await HttpClient.GetFromJsonAsync<List<UserDto>>(NavigationManager.BaseUri + "api/users") ?? new();
    }

    public async Task CreateUserAsync(UserDto item)
    {
        Users.Add(item);
        await HttpClient.PostAsJsonAsync(NavigationManager.BaseUri + "api/users/", item);
    }

    public async Task UpdateUserAsync(UserDto item)
    {
        await HttpClient.PutAsJsonAsync(NavigationManager.BaseUri + $"api/users/{item.Id}", item);
    }

    public async Task DeleteUserAsync(UserDto item)
    {
        Users.Remove(item);
        await HttpClient.DeleteAsync(NavigationManager.BaseUri + $"api/users/{item.Id}");
    }
}
