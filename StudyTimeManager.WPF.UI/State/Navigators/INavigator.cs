using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StudyTimeManager.WPF.UI.State.Navigators
{
    
    public interface INavigator
    {
        ObservableObject CurrentViewModel { get; set; }
        void UpdatedCurrentViewModelCommand(); 
    }
}
