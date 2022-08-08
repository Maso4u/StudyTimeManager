using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using StudyTimeManager.Domain.Extensions;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels;
public partial class CreateModuleStudySessionViewModel : ObservableValidator
{

    private readonly ObservableCollection<ModuleListingItemViewModel> _modules;
    
    [Required]
    [ObservableProperty]
    private DateTime _selectedDate;

    [Required]
    [ObservableProperty]
    private int hoursSpent;

    [ObservableProperty]
    private ModuleListingItemViewModel _selectedModuleListingItemViewModel;

    [ObservableProperty]
    private bool _canCreate;

    private bool _semesterCreated = false;
    private bool _moduleCreated = false;

    private readonly IServiceManager _service;

    public IEnumerable<ModuleListingItemViewModel> Modules => _modules;

    [ObservableProperty]
    private DateTime _semesterStartDate; 

    [ObservableProperty]
    private DateTime _semesterEndDate; 

    public ICommand AddStudySessionCommand { get; }

    public CreateModuleStudySessionViewModel(IServiceManager service)
    {
        _service = service;
        _modules = new ObservableCollection<ModuleListingItemViewModel>();
        _selectedDate = DateTime.Now;
        CanCreate = _semesterCreated && _moduleCreated;
        AddStudySessionCommand = new RelayCommand(Create);
        RegisterToSemesterCreatedMessage();
        RegisterToModuleCreatedMessage();
    }


    [RelayCommand]
    private void Create()
    {
        string moduleCode = _selectedModuleListingItemViewModel.ModuleCode;
        int week = DetermineWeekOfStudyStudySession();

        StudySession studySession = new StudySession()
        {
            Date = DateOnly.FromDateTime(SelectedDate),
            HoursSpent = HoursSpent
        };

        bool successful = _service.StudySessionService.CreateStudySession(moduleCode, week, studySession);
        if (successful)
        {
            if(UpdateSelfStudyHoursOfWeek(moduleCode, week, HoursSpent))
            {
                StudySessionCreatedMessage message = new StudySessionCreatedMessage(moduleCode);
                WeakReferenceMessenger.Default.Send(message);
            }
        }
    }

    private int DetermineWeekOfStudyStudySession()
    {
        string moduleCode = _selectedModuleListingItemViewModel.ModuleCode;
        Module module = _service.ModuleService.GetModule(moduleCode);

        int weekNumber = 1;
        foreach (var week in module.Weeks)
        {
            DateTime weekStartDate = week.StartDate.ToDateTime();
            DateTime weekEndDate = week.EndDate.ToDateTime();

            if (_selectedDate.IsBetweenTwoDates(weekStartDate, weekEndDate))
            {
                weekNumber = week.WeekNumber;
                break;
            }
        }

        return weekNumber;
    }

    private bool UpdateSelfStudyHoursOfWeek(string moduleCode, int week, int HoursSpent)
    {
        return _service.ModuleSemesterWeekService
            .UpdateSelfStudyHoursOfModuleSemesterWeek(moduleCode, week, HoursSpent);
    }
        
    private void RegisterToModuleCreatedMessage()
    {
        WeakReferenceMessenger.Default.Register<ModuleCreatedMessage>(this,
            (_createModuleStudySessionViewModel, message) =>
            {
                _moduleCreated = true;
                CanCreate = _semesterCreated && _moduleCreated;
                _modules.Add(new ModuleListingItemViewModel(message.Value));
            });
    }

    private void RegisterToSemesterCreatedMessage()
    {
        WeakReferenceMessenger.Default.Register<SemesterCreatedMessage>(this,
            (_createModuleStudySessionViewModel, message) =>
            {
                _semesterCreated = true;
                CanCreate = _semesterCreated && _moduleCreated;
                SemesterStartDate = _service.SemesterService.GetSemester().StartDate.ToDateTime();
                SemesterEndDate = _service.SemesterService.GetSemester().EndDate.ToDateTime();
            });
    }

}
