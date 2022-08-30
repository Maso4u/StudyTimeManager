using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using StudyTimeManager.Domain.Extensions;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels;
public partial class CreateModuleStudySessionViewModel : ObservableValidator, 
    IRecipient<ModuleDeletedMessage>,
    IRecipient<SemesterCreatedMessage>,
    IRecipient<ModuleCreatedMessage>
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

    [ObservableProperty]
    private DateTime _semesterStartDate;

    [ObservableProperty]
    private DateTime _semesterEndDate;


    private bool _semesterCreated = false;
    private bool _moduleCreated = false;
    public IEnumerable<ModuleListingItemViewModel> Modules => _modules;
    private readonly IServiceManager _service;
    public ISnackbarMessageQueue MessageQueue { get; }
    public CreateModuleStudySessionViewModel(IServiceManager service, 
        ISnackbarMessageQueue messageQueue)
    {
        _service = service;
        MessageQueue = messageQueue;
        _modules = new ObservableCollection<ModuleListingItemViewModel>();
        CanCreate = _semesterCreated && _moduleCreated;
        SelectedDate = SemesterStartDate;
        RegisterToMessages();
    }

    [RelayCommand]
    private void AddStudySession()
    {
        string? moduleCode = _selectedModuleListingItemViewModel.ModuleCode;
        int week = DetermineWeekOfStudyStudySession();

        StudySession studySession = new StudySession()
        {
            Date = DateOnly.FromDateTime(SelectedDate),
            HoursSpent = HoursSpent
        };

        bool successful = _service.StudySessionService
            .CreateStudySession(moduleCode, week, studySession);

        if (successful)
        {
            if (UpdateSelfStudyHoursOfWeek(moduleCode, week, HoursSpent))
            {
                StudySessionCreatedMessage message = new StudySessionCreatedMessage(moduleCode);
                WeakReferenceMessenger.Default.Send(message);
                MessageQueue.Enqueue("Study session registered successfully.", 
                    "UNDO", () => UndoStudySession(moduleCode,week,HoursSpent));
            }
        }
    }

    private void UndoStudySession(string moduleCode, int week, int HoursSpent)
    {
        DateOnly date = DateOnly.FromDateTime(SelectedDate);

        bool successful = _service.StudySessionService
            .RemoveStudySession(moduleCode, week, date);

        if (successful)
        {
            StudySessionRemovedMessage message = new StudySessionRemovedMessage(moduleCode);
            WeakReferenceMessenger.Default.Send(message);
        }
    }

    private void RegisterToMessages()
    {
        WeakReferenceMessenger.Default.Register<ModuleCreatedMessage>(this);
        WeakReferenceMessenger.Default.Register<ModuleDeletedMessage>(this);
        WeakReferenceMessenger.Default.Register<SemesterCreatedMessage>(this);
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

    private void RemoveModule(Module module)
    {
        ModuleListingItemViewModel moduleListingFound = _modules
            .FirstOrDefault(m => m.ModuleCode == module.Code);

        _modules.Remove(moduleListingFound);
    }

    public void Receive(ModuleDeletedMessage message)
    {
        RemoveModule(message.Value);
        if (Modules.Count() <= 0)
        {
            CanCreate = false;
        }
    }

    public void Receive(SemesterCreatedMessage message)
    {
        _semesterCreated = true;
        CanCreate = _semesterCreated && _moduleCreated;
        SemesterStartDate = _service.SemesterService.GetSemester().StartDate.ToDateTime();
        SemesterEndDate = _service.SemesterService.GetSemester().EndDate.ToDateTime();
    }

    public void Receive(ModuleCreatedMessage message)
    {
        SelectedDate = SemesterStartDate;
        _moduleCreated = true;
        CanCreate = _semesterCreated && _moduleCreated;
        _modules.Add(new ModuleListingItemViewModel(message.Value));
    }
}
