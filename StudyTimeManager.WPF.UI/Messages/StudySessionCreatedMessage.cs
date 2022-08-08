using CommunityToolkit.Mvvm.Messaging.Messages;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.WPF.UI.Messages
{
    internal class StudySessionCreatedMessage : ValueChangedMessage<string>
    {
        public StudySessionCreatedMessage(string value) : base(value)
        {
        }
    }
}
