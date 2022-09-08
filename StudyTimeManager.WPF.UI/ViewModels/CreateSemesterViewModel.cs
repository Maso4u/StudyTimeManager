using CommunityToolkit.Mvvm.ComponentModel;
using StudyTimeManager.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using StudyTimeManager.Domain.Services.Contracts;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using StudyTimeManager.WPF.UI.Messages;
using MaterialDesignThemes.Wpf;

namespace StudyTimeManager.WPF.UI.ViewModels;
/// <summary>
/// Abstraction of the view responsible for creation of a semester
/// </summary>
public partial class CreateSemesterViewModel : ObservableValidator
{
    /// <summary>
    /// Start date for a semester
    /// </summary>
    [Required(ErrorMessage ="Semester start date is required")]
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanCreate))]
    private DateTime _startDate;

    /// <summary>
    /// Number of weeks for the semester
    /// </summary>
    [Required]
    [Range(1, int.MaxValue,ErrorMessage ="Number of weeks must be min. 1")]
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanCreate))]
    private int _numberOfWeeks;

    /// <summary>
    /// Gets or Sets whether or not the semester can be created.
    /// </summary>
    public bool CanCreate => _numberOfWeeks>0 && !string.IsNullOrWhiteSpace(_startDate.ToString());

    private readonly IServiceManager _service;

    /// <summary>
    /// Manages messages queued to be displayed by a snackbar
    /// </summary>
    public ISnackbarMessageQueue MessageQueue { get; }

    public CreateSemesterViewModel(IServiceManager service, ISnackbarMessageQueue messageQueue)
    {
        MessageQueue = messageQueue;
        _startDate = DateTime.Now;
        _service = service;
    }

    /// <summary>
    /// Command that creates the semester
    /// </summary>
    [RelayCommand]
    private void CreateSemester()
    {
        //instantiate a new semester with the values of the properties and try create it
        Semester semester = new Semester()
        {
            NumberOfWeeks = NumberOfWeeks,
            StartDate = DateOnly.FromDateTime(StartDate)
        };
        bool successful = _service.SemesterService.CreateSemester(semester);

        //if semester has been successfully created
        //create and send a message on the successful creation of the semester.
        //add a message to the message queue to notify user that the operation has been successful
        if (successful)
        {
            SemesterCreatedMessage message = new SemesterCreatedMessage(successful);
            WeakReferenceMessenger.Default.Send(message);

            MessageQueue.Enqueue("Semester successfully created.", "UNDO", () => UndoCreate());
        }
    }

    /// <summary>
    /// Reverts the creation of the semester
    /// </summary>
    private void UndoCreate()
    {
        SemesterCreatedMessage message = new SemesterCreatedMessage(false);
        WeakReferenceMessenger.Default.Send(message);
    }
}
