using CommunityToolkit.Mvvm.Messaging.Messages;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class StudySessionRemovedMessage : ValueChangedMessage<string>
    {
        public StudySessionRemovedMessage(string value) : base(value)
        {
        }
    }
}
