using CommunityToolkit.Mvvm.Messaging.Messages;
using Shared.DTOs.Semester;
using System;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class SemesterCreatedMessage : ValueChangedMessage<SemesterDTO>
    {
        public SemesterCreatedMessage(SemesterDTO value) : base(value)
        {
        }
    }
}
