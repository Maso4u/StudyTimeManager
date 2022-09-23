using CommunityToolkit.Mvvm.Messaging.Messages;
using Shared.DTOs.Module;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class ModuleDeletedMessage : ValueChangedMessage<ModuleDTO>
    {
        public ModuleDeletedMessage(ModuleDTO value) : base(value)
        {
        }
    }
}
