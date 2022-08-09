using CommunityToolkit.Mvvm.Messaging.Messages;
using StudyTimeManager.WPF.UI.ViewModels;

namespace StudyTimeManager.WPF.UI.Messages
{
    internal class ModuleDeletedMessage : ValueChangedMessage<ModuleListingItemViewModel>
    {
        public ModuleDeletedMessage(ModuleListingItemViewModel value) : base(value)
        {
        }
    }
}
