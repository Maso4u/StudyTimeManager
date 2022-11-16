using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Shared.DTOs.Semester;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using StudyTimeManager.WPF.UI.State.Authenticators;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    /// <summary>
    /// Abstraction of the view responsible for creation of a semester
    /// </summary>
    public partial class SemesterViewModel : ObservableObject
    {
        /// <summary>
        /// Start date for a semester
        /// </summary>
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanCreate))]
        private string _startDate;

        [ObservableProperty]
        private string _endDate;

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
        private readonly IAuthenticator _authenticator;
        private SemesterDTO? userSemester;
        [ObservableProperty]
        private bool _userHasSemester;

        /// <summary>
        /// Manages messages queued to be displayed by a snackbar
        /// </summary>
        public ISnackbarMessageQueue MessageQueue { get; }
        public IAsyncRelayCommand DeleteSemesterCommand { get; }
        public IAsyncRelayCommand CreateSemesterCommand { get; }
        public ICommand Flip { get; }


        public SemesterViewModel(IServiceManager service,
            ISnackbarMessageQueue messageQueue,
            IAuthenticator authenticator,
            LoginViewModel loginViewModel)
        {
            Flip = Flipper.FlipCommand;
            MessageQueue = messageQueue;
            _startDate = DateTime.Now.ToString();
            _service = service;
            _authenticator = authenticator;
            CreateSemesterCommand = new AsyncRelayCommand(CreateSemester);
            DeleteSemesterCommand = new AsyncRelayCommand(DeleteSemester);
            loginViewModel.UserLoggedInSuccessfully += LoginViewModel_UserLoggedInSuccessfully;
        }

        private async void LoginViewModel_UserLoggedInSuccessfully()
        {
            userSemester = await _service.SemesterService
                .GetSemester(_authenticator.CurrentUser.Id, false);
            if (userSemester is null)
            {
                UserHasSemester = false;
                return;
            }
            UserHasSemester = true;
            StartDate = userSemester.StartDate.ToString("d");
            EndDate = userSemester.EndDate.ToString("d");
            NumberOfWeeks = userSemester.NumberOfWeeks;
            Flip.Execute(null);
            CurrentSemesterSetMessage message = new CurrentSemesterSetMessage(userSemester);
            WeakReferenceMessenger.Default.Send(message);
        }


        /// <summary>
        /// Command that creates the semester
        /// </summary>
        private async Task CreateSemester()
        {
            //instantiate a new semester with the values of the properties and try create it
            SemesterForCreationDTO semester = new()
            {
                NumberOfWeeks = NumberOfWeeks,
                StartDate = DateTime.Parse(StartDate)
            };
            userSemester = await _service.SemesterService
                .CreateSemester(_authenticator.CurrentUser.Id, semester);

            //if semester has been successfully created
            //create and send a message containing the Id of the semester.
            //add a message to the message queue to notify user that the operation has been successful
            if (userSemester != null)
            {
                StartDate = userSemester.StartDate.ToString("d");
                EndDate = userSemester.EndDate.ToString("d");
                CurrentSemesterSetMessage message = new CurrentSemesterSetMessage(userSemester);
                WeakReferenceMessenger.Default.Send(message);
                Flip.Execute(null);
            }
        }

        private async Task DeleteSemester()
        {
            await _service.SemesterService.DeleteSemester(userSemester.Id);

            Flip.Execute(null);
            UserHasSemester = false;
            SemesterDeletedMessage message = new SemesterDeletedMessage(userSemester);
            WeakReferenceMessenger.Default.Send(message);
        }
    }
}