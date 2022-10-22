using CommunityToolkit.Mvvm.Messaging.Messages;
using StudyTimeManager.WPF.UI.ViewModels;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class ChangeViewModelMessage : ValueChangedMessage<ViewType>
    {
        public ChangeViewModelMessage(ViewType value) : base(value)
        {
        }
    }
}
