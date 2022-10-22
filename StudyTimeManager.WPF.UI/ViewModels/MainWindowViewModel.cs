using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using StudyTimeManager.WPF.UI.Messages;
using StudyTimeManager.WPF.UI.State.Authenticators;
using StudyTimeManager.WPF.UI.State.Navigators;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    public enum ViewType
    {
        Register,
        Login,
        Dashboard
    }
    /// <summary>
    /// Abstraction for the view that hosts the other smaller views
    /// </summary>
    public partial class MainWindowViewModel : ObservableObject
    {

        public DashboardViewModel DashboardViewModel { get; }
        public LoginViewModel LoginViewModel { get; }
        public RegisterViewModel RegisterViewModel { get; }

        [ObservableProperty]
        public ObservableObject currentViewModel;
        /// <summary>
        /// Gets the message queue for the snackbar
        /// </summary>
        public ISnackbarMessageQueue MessageQueue { get; }
        public IAuthenticator Authenticator { get; }
        /*
                /// <summary>
                /// Gets or sets the create semester viewmodel
                /// </summary>
                public CreateSemesterViewModel CreateSemesterViewModel { get; set; }

                /// <summary>
                /// Gets or sets the create module viewmodel
                /// </summary>
                public CreateModuleViewModel CreateModuleViewModel { get; set; }

                /// <summary>
                /// Gets or sets the create module study session viewmodel
                /// </summary>
                public CreateModuleStudySessionViewModel CreateModuleStudySessionViewModel { get; set; }

                /// <summary>
                /// Gets or sets the module listing viewmodel
                /// </summary>
                public ModulesListingViewModel ModulesListingViewModel { get; set; }

                /// <summary>
                /// Gets or sets the module semester-week listing viewmodel
                /// </summary>
                public ModuleSemesterWeekListingViewModel ModuleSemesterWeekListingViewModel { get; set; }

                public MainWindowViewModel(CreateSemesterViewModel createSemesterViewModel,
                    CreateModuleViewModel createModuleViewModel,
                    CreateModuleStudySessionViewModel createModuleStudySessionViewModel,
                    ModulesListingViewModel modulesListingViewModel,
                    ModuleSemesterWeekListingViewModel moduleSemesterWeekListingViewModel,
                    ISnackbarMessageQueue messageQueue)
                {
                    CreateSemesterViewModel = createSemesterViewModel;
                    CreateModuleViewModel = createModuleViewModel;
                    CreateModuleStudySessionViewModel = createModuleStudySessionViewModel;
                    ModulesListingViewModel = modulesListingViewModel;
                    ModuleSemesterWeekListingViewModel = moduleSemesterWeekListingViewModel;

                    MessageQueue = messageQueue;
                }
        */


        public MainWindowViewModel(DashboardViewModel dashboardViewModel,
            LoginViewModel loginViewModel,
            RegisterViewModel registerViewModel,
            ISnackbarMessageQueue messageQueue,
            IAuthenticator authenticator)
        {
            RegisterToNavigationMessage();
            DashboardViewModel = dashboardViewModel;
            LoginViewModel = loginViewModel;
            MessageQueue = messageQueue;
            Authenticator = authenticator;
            CurrentViewModel = LoginViewModel;
            Authenticator = authenticator;
            RegisterViewModel = registerViewModel;
        }

        private void RegisterToNavigationMessage()
        {
            WeakReferenceMessenger.Default.Register<ChangeViewModelMessage>(this, (r, message) =>
            {
                navigate(message.Value);
            });
        }

        private void navigate(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    CurrentViewModel = LoginViewModel;
                    break;
                case ViewType.Register:
                    CurrentViewModel = RegisterViewModel;
                    break;
                case ViewType.Dashboard:
                    MessageQueue.Enqueue($"Welcome back, {Authenticator.CurrentUser.Username}.");
                    CurrentViewModel = DashboardViewModel;
                    break;
            }
        }
    }
}