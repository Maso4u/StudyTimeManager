using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Shared.DTOs.User;
using StudyTimeManager.WPF.UI.Messages;
using StudyTimeManager.WPF.UI.State.Authenticators;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string password;


        private bool canLogin;
        public bool CanLogin
        {
            get => canLogin;
            set
            {
                value = !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(UserName);
                SetProperty(ref canLogin, value);
            }
        }

        private readonly IAuthenticator _authenticator;
        /// <summary>
        /// Gets the message queue for the snackbar
        /// </summary>
        public ISnackbarMessageQueue MessageQueue { get; }
        public IAsyncRelayCommand LoginCommand { get; }

        public event Action UserLoggedInSuccessfully;

        public LoginViewModel(IAuthenticator authenticator,
            ISnackbarMessageQueue messageQueue)
        {
            _authenticator = authenticator;
            MessageQueue = messageQueue;
            LoginCommand = new AsyncRelayCommand(Login);
        }

        private async Task Login()
        {

            CanLogin = !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(UserName);
            if (!CanLogin)
            {
                MessageQueue.Enqueue("Please ensure that all fields are filled in");
                return;
            }

            UserForLoginDTO user = new UserForLoginDTO()
            {
                Username = UserName,
                Password = Password
            };
            bool successful = await _authenticator.Login(user);

            if (!successful)
            {
                MessageQueue.Enqueue("Login attempt failed");
                return;
            }

            ChangeViewModelMessage message = new ChangeViewModelMessage(ViewType.Dashboard);
            WeakReferenceMessenger.Default.Send(message);
            UserLoggedInSuccessfully.Invoke();
        }

        [RelayCommand]
        private void NavigateToRegister()
        {
            ChangeViewModelMessage message = new ChangeViewModelMessage(ViewType.Register);
            WeakReferenceMessenger.Default.Send(message);
        }
    }
}
