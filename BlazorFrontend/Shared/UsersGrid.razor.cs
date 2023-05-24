using ApiData.Models;
using BlazorFrontend.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace BlazorFrontend.Shared;

public partial class UsersGrid
{
    [Inject] private UserService UserService { get; set; } = null!;
    
    private RadzenDataGrid<UserDto> _grid = null!;
    private List<UserDto> Users { get; set; } = new();
    private bool _isLoading;
    private UserDto? _userToUpdate;
    private UserDto? _userToInsert;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        await LoadData(null);
    }

    async Task LoadData(LoadDataArgs? args)
    {
        _isLoading = true;

        await UserService.LoadUsersAsync();
        Users = UserService.Users;

        _isLoading = false;
    }

    async Task EditRow(UserDto user)
    {
        _userToUpdate = user;
        await _grid.EditRow(user);
    }

    async Task OnUpdateRow(UserDto user)
    {
        if (user == _userToInsert)
        {
            _userToInsert = null;
        }

        _userToUpdate = null;
        await UserService.UpdateUserAsync(user);
    }

    async Task SaveRow(UserDto user)
    {
        _isLoading = true;
        await _grid.UpdateRow(user);
        _isLoading = false;
    }

    void CancelEdit(UserDto user)
    {
        _userToUpdate = null;
        _grid.CancelEditRow(user);
    }

    async Task DeleteRow(UserDto user)
    {
        _isLoading = true;
        if (user == _userToInsert)
        {
            _userToInsert = null;
        }

        if (user == _userToUpdate)
        {
            _userToUpdate = null;
        }

        if (Users.Contains(user))
        {
            await UserService.DeleteUserAsync(user);
            await _grid.Reload();
        }
        else
        {
            _grid.CancelEditRow(user);
        }
        
        _isLoading = false;
    }

    async Task InsertRow()
    {
        _userToInsert = new UserDto();
        await _grid.InsertRow(_userToInsert);
    }

    async Task OnCreateRow(UserDto user)
    {
        await UserService.CreateUserAsync(user);
        _userToInsert = null;
    }


}
