using CommunityToolkit.Mvvm.Messaging.Messages;
using System;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class StudySessionRemovedMessage : ValueChangedMessage<Guid>
    {
        public StudySessionRemovedMessage(Guid moduleId) : base(moduleId)
        {
        }
    }
}
