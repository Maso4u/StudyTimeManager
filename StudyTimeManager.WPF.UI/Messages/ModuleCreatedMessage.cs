using CommunityToolkit.Mvvm.Messaging.Messages;
using Shared.DTOs.Module;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class ModuleCreatedMessage : ValueChangedMessage<ModuleDTO>
    {
        public ModuleCreatedMessage(ModuleDTO value) : base(value)
        {
        }
    }
}
