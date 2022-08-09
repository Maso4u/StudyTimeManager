using CommunityToolkit.Mvvm.Messaging.Messages;
using StudyTimeManager.WPF.UI.ViewModels;

namespace StudyTimeManager.WPF.UI.Messages
{
    internal class SelectedModuleListingItemViewModelChangedMessage : ValueChangedMessage<ModuleListingItemViewModel?>
    {
        public SelectedModuleListingItemViewModelChangedMessage(ModuleListingItemViewModel? value) : base(value)
        {
        }
    }
}
