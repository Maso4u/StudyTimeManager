using CommunityToolkit.Mvvm.ComponentModel;
using StudyTimeManager.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using StudyTimeManager.Services.Contracts;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using StudyTimeManager.WPF.UI.Messages;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Semester;

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
            SemesterDTO semesterDTO = _service.SemesterService.CreateSemester(semester);


            //if semester has been successfully created
            //create and send a message containing the Id of the semester.
            //add a message to the message queue to notify user that the operation has been successful
            if (semesterDTO != null)
            {
                SemesterCreatedMessage message = new SemesterCreatedMessage(semesterDTO);
                WeakReferenceMessenger.Default.Send(message);

                MessageQueue.Enqueue("Semester successfully created.", "UNDO", () => UndoCreate());
            }
        }

        /// <summary>
        /// Reverts the creation of the semester
        /// </summary>
        private void UndoCreate()
        {

            //SemesterCreatedMessage message = new SemesterCreatedMessage(false);
            //WeakReferenceMessenger.Default.Send(message);
        }
    }
}