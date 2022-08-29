using CommunityToolkit.Mvvm.Messaging.Messages;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.WPF.UI.Messages
{
    public class ModuleDeletedMessage : ValueChangedMessage<Module>
    {
        public ModuleDeletedMessage(Module value) : base(value)
        {
        }
    }
}
