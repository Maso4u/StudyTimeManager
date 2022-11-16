using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the create semester viewmodel
        /// </summary>
        public SemesterViewModel SemesterViewModel { get; set; }

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

        /// <summary>
        /// Gets the message queue for the snackbar
        /// </summary>
        public ISnackbarMessageQueue MessageQueue { get; }
        public DashboardViewModel
        (
            SemesterViewModel semesterViewModel,
            CreateModuleViewModel createModuleViewModel,
            CreateModuleStudySessionViewModel createModuleStudySessionViewModel,
            ModulesListingViewModel modulesListingViewModel,
            ModuleSemesterWeekListingViewModel moduleSemesterWeekListingViewModel,
            ISnackbarMessageQueue messageQueue)
        {
            SemesterViewModel = semesterViewModel;
            CreateModuleViewModel = createModuleViewModel;
            CreateModuleStudySessionViewModel = createModuleStudySessionViewModel;
            ModulesListingViewModel = modulesListingViewModel;
            ModuleSemesterWeekListingViewModel = moduleSemesterWeekListingViewModel;

            MessageQueue = messageQueue;
        }
    }
}
