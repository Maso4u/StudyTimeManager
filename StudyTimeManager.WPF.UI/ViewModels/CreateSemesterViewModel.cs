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
public partial class CreateSemesterViewModel : ObservableValidator
{
    [Required(ErrorMessage ="Semester start date is required")]
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanCreate))]
    private DateTime _startDate;

    [Required]
    [Range(1, int.MaxValue,ErrorMessage ="Number of weeks must be min. 1")]
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanCreate))]
    private int _numberOfWeeks;
    public bool CanCreate => _numberOfWeeks>0 && !string.IsNullOrWhiteSpace(_startDate.ToString());

    private readonly IServiceManager _service;

    public ISnackbarMessageQueue MessageQueue { get; }

    public CreateSemesterViewModel(IServiceManager service, ISnackbarMessageQueue messageQueue)
    {
        Semester semester = new Semester();
        MessageQueue = messageQueue;
        _startDate = DateTime.Now;
        _service = service;
    }

    [RelayCommand]
    private void CreateSemester()
    {
        Semester semester = new Semester()
        {
            NumberOfWeeks = _numberOfWeeks,
            StartDate = DateOnly.FromDateTime(_startDate)
        };

        bool successful = _service.SemesterService.CreateSemester(semester);

        if (successful)
        {
            SemesterCreatedMessage message = new SemesterCreatedMessage(successful);
            WeakReferenceMessenger.Default.Send(message);

            MessageQueue.Enqueue("Semester successfully created.", "UNDO", () => UndoCreate());
        }
    }

    private void UndoCreate()
    {
        SemesterCreatedMessage message = new SemesterCreatedMessage(false);
        WeakReferenceMessenger.Default.Send(message);
    }
}
