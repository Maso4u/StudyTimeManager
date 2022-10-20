using CommunityToolkit.Mvvm.Messaging.Messages;
using Shared.DTOs.Semester;
using System;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class SemesterDeletedMessage : ValueChangedMessage<SemesterDTO>
    {
        public SemesterDeletedMessage(SemesterDTO value) : base(value)
        {
        }
    }
}
