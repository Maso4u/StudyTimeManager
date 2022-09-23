using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Shared.DTOs.Module;
using Shared.DTOs.Semester;
using Shared.DTOs.StudySession;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
    private SemesterDTO semester;
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
        //instantiate a new study session
        StudySessionForCreationDTO studySession = new()
        {
            Date = DateOnly.FromDateTime(SelectedDate),
            HoursSpent = HoursSpent
        };

        //try and create a study session
        StudySessionDTO studySessionDTO = _service.StudySessionService
            .CreateStudySession(SelectedModuleListingItemViewModel.Id, studySession);
        if (studySessionDTO is null)
        {
            MessageQueue.Enqueue(
                $"Attempt to add a new study session for " +
                $"{SelectedModuleListingItemViewModel.ModuleName}, " +
                $"for the date {SelectedDate} was unsuccessful.");
            return;
        }

        StudySessionCreatedMessage message = new StudySessionCreatedMessage(SelectedModuleListingItemViewModel.Id);
        WeakReferenceMessenger.Default.Send(message);

        MessageQueue.Enqueue("A new study session has been successfully added.");
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
    /// Removes module listing item view model from the observable collection of them
    /// </summary>
    /// <param name="module">
    /// the module for which a module listing item view model must be removed
    /// </param>
    private void RemoveModule(ModuleDTO module)
    {
        //find the module listing item view model of
        //the module to be removed from observable collection
        ModuleListingItemViewModel? moduleListingFound = _modules
            .FirstOrDefault(m => m.Id == module.Id);
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
        SemesterStartDate = message.Value.StartDate.ToDateTime(TimeOnly.MinValue);
        SemesterEndDate = message.Value.EndDate.ToDateTime(TimeOnly.MinValue);
    }

    public void Receive(ModuleCreatedMessage message)
    {
        SelectedDate = SemesterStartDate;
        _moduleCreated = true;
        CanCreate = _semesterCreated && _moduleCreated;
        //add a new module listing item view model with the value of the message
        //(a ModuleDTO) as it argument
        _modules.Add(new ModuleListingItemViewModel(message.Value));
    }
}
