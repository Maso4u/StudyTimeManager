using CommunityToolkit.Mvvm.Messaging.Messages;
using Shared.DTOs.Semester;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class SelectedSemesterChangedMessage : ValueChangedMessage<SemesterDTO>
    {
        public SelectedSemesterChangedMessage(SemesterDTO value) : base(value)
        {
        }
    }
}
