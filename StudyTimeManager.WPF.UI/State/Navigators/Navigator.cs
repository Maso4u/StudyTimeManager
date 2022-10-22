using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StudyTimeManager.WPF.UI.State.Navigators
{
    internal class Navigator : INavigator
    {
        public ObservableObject CurrentViewModel { get; set; }

        public IRelayCommand UpdatedCurrentViewModelCommand => throw new System.NotImplementedException();

        void INavigator.UpdatedCurrentViewModelCommand()
        {
            throw new System.NotImplementedException();
        }
    }
}
