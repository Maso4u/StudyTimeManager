using CommunityToolkit.Mvvm.Messaging.Messages;
using Shared.DTOs.Module;
using StudyTimeManager.Domain.Models;
using System.Collections.Generic;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class SemesterModulesFoundMessage : ValueChangedMessage<IEnumerable<ModuleDTO>>
    {
        public SemesterModulesFoundMessage(IEnumerable<ModuleDTO> value) : base(value)
        {
        }
    }
}
