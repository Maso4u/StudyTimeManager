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

/// <summary>
/// Abstraction of the view responsible for the creation of a module
/// </summary>
public partial class CreateModuleStudySessionViewModel : ObservableValidator, 
    IRecipient<ModuleDeletedMessage>,
    IRecipient<SemesterCreatedMessage>,
    IRecipient<ModuleCreatedMessage>
{
    /// <summary>
    /// Observable collection of module listing view model items
    /// </summary>
    private readonly ObservableCollection<ModuleListingItemViewModel> _modules;

    /// <summary>
    /// Gets or sets the selected date for the study session
    /// </summary>
    [Required]
    [ObservableProperty]
    private DateTime _selectedDate;

    /// <summary>
    /// Gets or sets the the hours spent studying in the session
    /// </summary>
    [Required]
    [ObservableProperty]
    private int _hoursSpent;

    /// <summary>
    /// Gets or sets the module the study session is created for
    /// </summary>
    [ObservableProperty]
    private ModuleListingItemViewModel _selectedModuleListingItemViewModel;

    /// <summary>
    /// Gets or sets whether or not a study session can be created
    /// </summary>
    [ObservableProperty]
    private bool _canCreate;

    /// <summary>
    /// Gets or sets the start date of the semester
    /// </summary>
    [ObservableProperty]
    private DateTime _semesterStartDate;

    /// <summary>
    /// Gets or sets the end date of the semester
    /// </summary>
    [ObservableProperty]
    private DateTime _semesterEndDate;


    private bool _semesterCreated = false;
    private bool _moduleCreated = false;

    /// <summary>
    /// Gets the enumerable collection of module listing item viewmodels
    /// </summary>
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

    /// <summary>
    /// Command that creates a study session
    /// </summary>
    [RelayCommand]
    private void AddStudySession()
    {
        //get the code of the selected module listing item
        //as well determine the week the study session is being added on
        //and assign the value to local variable
        string? moduleCode = SelectedModuleListingItemViewModel.ModuleCode;
        int week = DetermineWeekOfStudyStudySession();
        //instantiate a new study session
        StudySession studySession = new()
        {
            Date = DateOnly.FromDateTime(SelectedDate),
            HoursSpent = HoursSpent
        };

        //try and create a study session
        bool successful = _service.StudySessionService
            .CreateStudySession(moduleCode, week, studySession);
        if (successful)
        {
            //if the study session was successfuly created
            //try and update the self study hours of the week it was added on.
            // if that is done successfuly create and send a study session created message
            if (UpdateSelfStudyHoursOfWeek(moduleCode, week, HoursSpent))
            {
                StudySessionCreatedMessage message = new StudySessionCreatedMessage(moduleCode);
                WeakReferenceMessenger.Default.Send(message);

                //add a message to the message queue
                //to notify user that the operation has been successful
                MessageQueue.Enqueue("Study session registered successfully.", 
                    "UNDO", () => UndoStudySession(moduleCode,week));
            }
        }
    }

    /// <summary>
    /// Undoes the creation of a study session
    /// </summary>
    /// <param name="moduleCode">
    /// Code of the module for which a study session is to be undone for
    /// </param>
    /// <param name="week">
    /// The week number in the semester for which the study session was created on
    /// </param>
    private void UndoStudySession(string moduleCode, int week)
    {
        DateOnly date = DateOnly.FromDateTime(SelectedDate);

        //try and remove a study session and if operation is successful
        //create and send a message that a study session has been removed
        bool successful = _service.StudySessionService
            .RemoveStudySession(moduleCode, week, date);
        if (successful)
        {
            StudySessionRemovedMessage message = new(moduleCode);
            WeakReferenceMessenger.Default.Send(message);
        }
    }

    /// <summary>
    /// Registers this view model to messages that it should receive
    /// </summary>
    private void RegisterToMessages()
    {
        WeakReferenceMessenger.Default.Register<ModuleCreatedMessage>(this);
        WeakReferenceMessenger.Default.Register<ModuleDeletedMessage>(this);
        WeakReferenceMessenger.Default.Register<SemesterCreatedMessage>(this);
    }

    /// <summary>
    /// Gets the week in the semester that the study session is being created for.
    /// </summary>
    /// <returns>The week number</returns>
    private int DetermineWeekOfStudyStudySession()
    {
        //get code of the moule that has been selected.
        //get the module by the code
        string? moduleCode = SelectedModuleListingItemViewModel.ModuleCode;
        Module module = _service.ModuleService.GetModule(moduleCode);

        //week number to be returned, with a default value being 1
        int weekNumber = 1;

        //iterate through the weeks of the module
        foreach (var week in module.Weeks)
        {
            
            DateTime weekStartDate = week.StartDate.ToDateTime();
            DateTime weekEndDate = week.EndDate.ToDateTime();
            //if the selected date is between the the start and end date of the current week
            //then that means the date is in the current week and the value week number of the current week
            //is assigned to the local variable weekNumber to be returned by this method.
            //break away from the foreach loop
            if (SelectedDate.IsBetweenTwoDates(weekStartDate, weekEndDate))
            {
                weekNumber = week.WeekNumber;
                break;
            }
        }

        return weekNumber;
    }

    /// <summary>
    /// Updates the self study hours of a modules semester week 
    /// that a study session has been created on
    /// </summary>
    /// <param name="moduleCode">The code for which a study session was added</param>
    /// <param name="week">The week on which a study session was created</param>
    /// <param name="HoursSpent">The hours spent during a study session</param>
    /// <returns></returns>
    private bool UpdateSelfStudyHoursOfWeek(string moduleCode, int week, int HoursSpent)
    {
        return _service.ModuleSemesterWeekService
            .UpdateSelfStudyHoursOfModuleSemesterWeek(moduleCode, week, HoursSpent);
    }

    /// <summary>
    /// Removes module listing item view model from the observable collection of them
    /// </summary>
    /// <param name="module">
    /// the module for which a module listing item view model must be removed
    /// </param>
    private void RemoveModule(Module module)
    {
        //find the module listing item view model of
        //the module to be removed from observable collection
        ModuleListingItemViewModel moduleListingFound = _modules
            .FirstOrDefault(m => m.ModuleCode == module.Code);
        //remove the found module listing item view model from
        //the observable collection it was found in
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
        //assign values to semester start and end date
        SemesterStartDate = _service.SemesterService.GetSemester()
            .StartDate.ToDateTime();
        SemesterEndDate = _service.SemesterService.GetSemester()
            .EndDate.ToDateTime();
    }

    public void Receive(ModuleCreatedMessage message)
    {
        SelectedDate = SemesterStartDate;
        _moduleCreated = true;
        CanCreate = _semesterCreated && _moduleCreated;
        //add a new module listing item view model with the value of the message
        //(a Module) as it argument
        _modules.Add(new ModuleListingItemViewModel(message.Value));
    }
}
