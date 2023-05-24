using ApiData.Models;
using BlazorFrontend.Data;
using BlazorFrontend.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace BlazorFrontend.Shared;

public partial class TeachersGrid
{
    [Inject] private SubjectMemberService SubjectMemberService { get; set; } = null!;
    
    [Parameter]
    public SubjectDto? SubjectDto { get; set; }

    private RadzenDataGrid<Teacher> _grid = null!;
    private List<Teacher> Teachers { get; set; } = new();
    private List<UserDto> Users { get; set; } = new();
    private bool _isLoading;
    private Teacher? _studentToUpdate;
    private Teacher? _studentToInsert;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        await LoadData(null);
    }

    async Task LoadData(LoadDataArgs? args)
    {
        _isLoading = true;

        if (SubjectDto != null)
        {
            await SubjectMemberService.LoadMembersAsync(SubjectDto.Id);
            Teachers = SubjectMemberService.Teachers;
            Users = SubjectMemberService.Users;
        }

        _isLoading = false;
    }

    async Task EditRow(Teacher student)
    {
        _studentToUpdate = student;
        await _grid.EditRow(student);
    }

    async Task OnUpdateRow(Teacher student)
    {
        if (student == _studentToInsert)
        {
            _studentToInsert = null;
        }

        _studentToUpdate = null;
        await SubjectMemberService.UpdateMemberAsync(student);
    }

    async Task SaveRow(Teacher student)
    {
        _isLoading = true;
        await _grid.UpdateRow(student);
        _isLoading = false;
    }

    void CancelEdit(Teacher student)
    {
        _studentToUpdate = null;
        _studentToInsert = null;
        _grid.CancelEditRow(student);
    }

    async Task DeleteRow(Teacher student)
    {
        _isLoading = true;
        if (student == _studentToInsert)
        {
            _studentToInsert = null;
        }

        if (student == _studentToUpdate)
        {
            _studentToUpdate = null;
        }

        if (Teachers.Contains(student))
        {
            await SubjectMemberService.DeleteMemberAsync(student);
        }
        else
        {
            _grid.CancelEditRow(student);
        }

        await _grid.Reload();
        _isLoading = false;
    }

    async Task InsertRow()
    {
        _studentToInsert = new Teacher();
        await _grid.InsertRow(_studentToInsert);
    }

    async Task OnCreateRow(Teacher student)
    {
        await SubjectMemberService.CreateMemberAsync(student);
        _studentToInsert = null;
    }

}
