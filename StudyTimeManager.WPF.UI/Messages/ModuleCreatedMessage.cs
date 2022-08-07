using CommunityToolkit.Mvvm.Messaging.Messages;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.WPF.UI.Messages
{
    internal class ModuleCreatedMessage : ValueChangedMessage<Module>
    {
        public ModuleCreatedMessage(Module value) : base(value)
        {
        }
    }
}
