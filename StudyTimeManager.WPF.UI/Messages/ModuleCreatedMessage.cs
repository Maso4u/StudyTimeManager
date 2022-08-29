using CommunityToolkit.Mvvm.Messaging.Messages;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class ModuleCreatedMessage : ValueChangedMessage<Module>
    {
        public ModuleCreatedMessage(Module value) : base(value)
        {
        }
    }
}
