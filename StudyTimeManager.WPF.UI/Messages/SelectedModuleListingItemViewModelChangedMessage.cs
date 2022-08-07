using CommunityToolkit.Mvvm.Messaging.Messages;

namespace StudyTimeManager.WPF.UI.Messages
{
    internal class SelectedModuleListingItemViewModelChangedMessage : ValueChangedMessage<string?>
    {
        public SelectedModuleListingItemViewModelChangedMessage(string? value) : base(value)
        {
        }
    }
}
