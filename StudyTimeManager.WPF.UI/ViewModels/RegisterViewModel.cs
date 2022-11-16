using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Shared.DTOs.User;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using StudyTimeManager.WPF.UI.State.Authenticators;
using System.Threading.Tasks;

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
        public IAsyncRelayCommand RegisterCommand { get; }

        public RegisterViewModel(IAuthenticator authenticator, ISnackbarMessageQueue messageQueue)
        {
            _authenticator = authenticator;
            MessageQueue = messageQueue;
            RegisterCommand = new AsyncRelayCommand(Register);
        }

        private async Task Register()
        {

            if (string.IsNullOrEmpty(UserName) && 
                string.IsNullOrEmpty(Password) && 
                string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageQueue.Enqueue("Please ensure that all fields are filled in");
                return;
            }

            UserForRegisterationDTO user = new UserForRegisterationDTO()
            { 
                Username = UserName,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };

            RegisterationResult result = await _authenticator.Register(user);

            switch (result)
            {
                case RegisterationResult.Success:
                    MessageQueue.Enqueue(
                            "Successfully registered! Do you wish to login?",
                            "LOGIN", () => NavigateToLogin());
                    break;
                case RegisterationResult.PasswordsDoNotMatch:
                    MessageQueue.Enqueue("Passwords entered do not match.");
                    break;
                case RegisterationResult.UsernameAlreadyExists:
                    MessageQueue.Enqueue($"Sorry. The username \"{UserName}\" is already taken.");
                    break;
            }
        }

        [RelayCommand]
        private void NavigateToLogin()
        {
            ChangeViewModelMessage message = new ChangeViewModelMessage(ViewType.Login);
            WeakReferenceMessenger.Default.Send(message);
        }
    }
}
