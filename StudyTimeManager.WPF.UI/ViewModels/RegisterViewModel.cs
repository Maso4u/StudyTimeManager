using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Shared.DTOs.User;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using StudyTimeManager.WPF.UI.State.Authenticators;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private bool canRegister;

        private readonly IAuthenticator _authenticator;
        /// <summary>
        /// Gets the message queue for the snackbar
        /// </summary>
        public ISnackbarMessageQueue MessageQueue { get; }

        public RegisterViewModel(IAuthenticator authenticator, ISnackbarMessageQueue messageQueue)
        {
            _authenticator = authenticator;
            MessageQueue = messageQueue;
        }
        [RelayCommand]
        private void Register()
        {
            UserForRegisterationDTO user = new UserForRegisterationDTO()
            { 
                Username = UserName,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };

            RegisterationResult result = _authenticator.Register(user);

            switch (result)
            {
                case RegisterationResult.Success:
                    SuccessfulRegisteration();
                    break;
                case RegisterationResult.PasswordsDoNotMatch:

                    break;
                case RegisterationResult.UsernameAlreadyExists:
                    UsernameTaken();
                    break;
            }
        }

        [RelayCommand]
        private void NavigateToLogin()
        {
            ChangeViewModelMessage message = new ChangeViewModelMessage(ViewType.Login);
            WeakReferenceMessenger.Default.Send(message);
        }

        private void SuccessfulRegisteration()
        {
            MessageQueue.Enqueue(
                            "Successfully registered! Do you wish to login?",
                            "LOGIN",()=>NavigateToLogin());

        }

        private void UsernameTaken()
        {
            MessageQueue.Enqueue(
                $"Sorry. The username \"{UserName}\" is already taken.");
        }
    }
}
