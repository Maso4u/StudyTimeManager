using CommunityToolkit.Mvvm.Messaging.Messages;
using StudyTimeManager.WPF.UI.ViewModels;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class SelectedModuleListingItemViewModelChangedMessage : ValueChangedMessage<ModuleListingItemViewModel?>
    {
        public SelectedModuleListingItemViewModelChangedMessage(ModuleListingItemViewModel? value) : base(value)
        {
        }
    }
}
