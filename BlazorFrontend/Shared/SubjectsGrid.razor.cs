using ApiData.Models;
using BlazorFrontend.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace BlazorFrontend.Shared;

public partial class SubjectsGrid
{
    [Inject] private SubjectService SubjectService { get; set; } = null!;
    
    private RadzenDataGrid<SubjectDto> _grid = null!;
    private List<SubjectDto> Subjects { get; set; } = new();
    private bool _isLoading;
    private SubjectDto? _subjectToUpdate;
    private SubjectDto? _subjectToInsert;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        await LoadData(null);
    }

    async Task LoadData(LoadDataArgs? args)
    {
        _isLoading = true;

        await SubjectService.LoadSubjectsAsync();
        Subjects = SubjectService.Subjects;

        _isLoading = false;
    }

    async Task EditRow(SubjectDto subject)
    {
        _subjectToUpdate = subject;
        await _grid.EditRow(subject);
    }

    async Task OnUpdateRow(SubjectDto subject)
    {
        if (subject == _subjectToInsert)
        {
            _subjectToInsert = null;
        }

        _subjectToUpdate = null;
        await SubjectService.UpdateSubjectAsync(subject);
    }

    async Task SaveRow(SubjectDto subject)
    {
        _isLoading = true;
        await _grid.UpdateRow(subject);
        _isLoading = false;
    }

    void CancelEdit(SubjectDto subject)
    {
        _subjectToUpdate = null;
        _subjectToInsert = null;
        _grid.CancelEditRow(subject);
    }

    async Task DeleteRow(SubjectDto subject)
    {
        _isLoading = true;
        if (subject == _subjectToInsert)
        {
            _subjectToInsert = null;
        }

        if (subject == _subjectToUpdate)
        {
            _subjectToUpdate = null;
        }

        if (Subjects.Contains(subject))
        {
            await SubjectService.DeleteSubjectAsync(subject);
        }
        else
        {
            _grid.CancelEditRow(subject);
        }

        await _grid.Reload();
        _isLoading = false;
    }

    async Task InsertRow()
    {
        _subjectToInsert = new SubjectDto();
        await _grid.InsertRow(_subjectToInsert);
    }

    async Task OnCreateRow(SubjectDto subject)
    {
        await SubjectService.CreateSubjectAsync(subject);
        _subjectToInsert = null;
    }
}
