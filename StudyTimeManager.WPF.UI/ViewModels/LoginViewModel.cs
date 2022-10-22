using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Shared.DTOs.User;
using StudyTimeManager.WPF.UI.Messages;
using StudyTimeManager.WPF.UI.State.Authenticators;
using StudyTimeManager.WPF.UI.State.Navigators;
using System.Runtime.Serialization;
using System.Security.Authentication;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    public partial class LoginViewModel: ObservableObject
    {
        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private bool canLogin;

        private readonly IAuthenticator _authenticator;
        /// <summary>
        /// Gets the message queue for the snackbar
        /// </summary>
        public ISnackbarMessageQueue MessageQueue { get; }

        public LoginViewModel(IAuthenticator authenticator, 
            ISnackbarMessageQueue messageQueue)
        {
            CanLogin = true;
            _authenticator = authenticator;
            MessageQueue = messageQueue;
        }

        [RelayCommand]
        private void Login()
        {
            UserForLoginDTO user = new UserForLoginDTO()
                {
                    Username = UserName,
                    Password = Password
                };
            bool successful = _authenticator.Login(user);

            if (!successful)
            {
                MessageQueue.Enqueue("Login attempt failed");
                return;
            }
            
            ChangeViewModelMessage message = new ChangeViewModelMessage(ViewType.Dashboard);
            WeakReferenceMessenger.Default.Send(message);
        }

        [RelayCommand]
        private void NavigateToRegister()
        {
            ChangeViewModelMessage message = new ChangeViewModelMessage(ViewType.Register);
            WeakReferenceMessenger.Default.Send(message);
        }
    }
}
