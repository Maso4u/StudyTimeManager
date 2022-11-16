using CommunityToolkit.Mvvm.Messaging.Messages;
using Shared.DTOs.Semester;
using System;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class CurrentSemesterSetMessage : ValueChangedMessage<SemesterDTO>
    {
        public CurrentSemesterSetMessage(SemesterDTO value) : base(value)
        {
        }
    }
}
