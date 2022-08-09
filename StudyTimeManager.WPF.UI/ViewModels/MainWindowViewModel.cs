using CommunityToolkit.Mvvm.ComponentModel;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public CreateSemesterViewModel CreateSemesterViewModel { get; set; }
        public CreateModuleViewModel CreateModuleViewModel { get; set; }
        public CreateModuleStudySessionViewModel CreateModuleStudySessionViewModel { get; set; }
        public ModulesListingViewModel ModulesListingViewModel { get; set; }
        public ModuleSemesterWeekListingViewModel ModuleSemesterWeekListingViewModel { get; set; }
        public MainWindowViewModel(CreateSemesterViewModel createSemesterViewModel,
            CreateModuleViewModel createModuleViewModel,
            CreateModuleStudySessionViewModel createModuleStudySessionViewModel,
            ModulesListingViewModel modulesListingViewModel,
            ModuleSemesterWeekListingViewModel moduleSemesterWeekListingViewModel)
        {
            CreateSemesterViewModel = createSemesterViewModel;
            CreateModuleViewModel = createModuleViewModel;
            CreateModuleStudySessionViewModel = createModuleStudySessionViewModel;
            ModulesListingViewModel = modulesListingViewModel;
            ModuleSemesterWeekListingViewModel = moduleSemesterWeekListingViewModel;
        }
    }
}
