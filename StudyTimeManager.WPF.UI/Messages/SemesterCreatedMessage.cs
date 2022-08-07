using CommunityToolkit.Mvvm.Messaging.Messages;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class SemesterCreatedMessage : ValueChangedMessage<bool>
    {
        public SemesterCreatedMessage(bool value) : base(value)
        {
        }
    }
}
