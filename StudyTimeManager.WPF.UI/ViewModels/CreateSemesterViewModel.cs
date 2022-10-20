using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Shared.DTOs.Semester;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    /// <summary>
    /// Abstraction of the view responsible for creation of a semester
    /// </summary>
    public partial class CreateSemesterViewModel : ObservableObject
    {
        /// <summary>
        /// Start date for a semester
        /// </summary>
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanCreate))]
        private DateTime _startDate;

        /// <summary>
        /// Number of weeks for the semester
        /// </summary>
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanCreate))]
        private int _numberOfWeeks;

        /// <summary>
        /// Gets or Sets whether or not the semester can be created.
        /// </summary>
        public bool CanCreate => _numberOfWeeks > 0 && !string.IsNullOrWhiteSpace(_startDate.ToString());

        private readonly IServiceManager _service;
        private SemesterDTO semesterDTO;

        /// <summary>
        /// Manages messages queued to be displayed by a snackbar
        /// </summary>
        public ISnackbarMessageQueue MessageQueue { get; }

        public CreateSemesterViewModel(IServiceManager service,
            ISnackbarMessageQueue messageQueue)
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
            SemesterForCreationDTO semester = new()
            {
                NumberOfWeeks = NumberOfWeeks,
                StartDate = StartDate
            };
            semesterDTO = _service.SemesterService.CreateSemester(semester);

            //if semester has been successfully created
            //create and send a message containing the Id of the semester.
            //add a message to the message queue to notify user that the operation has been successful
            if (semesterDTO != null)
            {
                SemesterCreatedMessage message = new SemesterCreatedMessage(semesterDTO);
                WeakReferenceMessenger.Default.Send(message);

                MessageQueue.Enqueue(
                    "Semester successfully created.",
                    "UNDO", 
                    () => UndoCreate(semesterDTO));
            }
        }

        [RelayCommand]
        private void DeleteSemester() 
        {
            UndoCreate(semesterDTO);
        }

        /// <summary>
        /// Reverts the creation of the semester
        /// </summary>
        private void UndoCreate(SemesterDTO semester)
        {
            _service.SemesterService.DeleteSemester(semester.Id);
            SemesterDeletedMessage message = new SemesterDeletedMessage(semester);
            WeakReferenceMessenger.Default.Send(message);
        }
    }
}