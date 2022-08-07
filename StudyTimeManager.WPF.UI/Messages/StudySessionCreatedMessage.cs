using CommunityToolkit.Mvvm.Messaging.Messages;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.WPF.UI.Messages
{
    internal class StudySessionCreatedMessage : ValueChangedMessage<StudySession>
    {
        public StudySessionCreatedMessage(StudySession value) : base(value)
        {
        }
    }
}
