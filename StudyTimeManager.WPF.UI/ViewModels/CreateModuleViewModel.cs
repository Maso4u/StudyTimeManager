using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    public partial class CreateModuleViewModel : ObservableValidator
    {

        [Required]
        [ObservableProperty]
        private string _moduleCode;

        [Required]
        [ObservableProperty]
        private string _moduleName;

        [Required]
        [ObservableProperty]
        [Range(1, int.MaxValue)]
        private int _numberOfCredits;

        [Required]
        [Range(1, int.MaxValue)]
        [ObservableProperty]
        private int _classHoursPerWeek;

        [ObservableProperty]
        public bool _canCreate = false;

        private readonly IServiceManager _service;

        public ICommand CreateModuleCommand { get; }

        public CreateModuleViewModel(IServiceManager service)
        {
            _service = service;
            WeakReferenceMessenger.Default.Register<SemesterCreatedMessage>(this,
                (_createModuleViewModel, message) => { OnSemesterCreated(message); });
            CreateModuleCommand =new RelayCommand(Create);
        }

        [RelayCommand]
        public void Create()
        {
            Module module = new Module()
            {
                Code = _moduleCode,
                Name = _moduleName,
                NumberOfCredits = _numberOfCredits,
                ClassHoursPerWeek = _classHoursPerWeek
            };
            bool successful = _service.ModuleService.CreateModule(module);

            if (successful)
            {
                SendMessage(module);
            }

        }

        private void SendMessage(Module module)
        {
            ModuleCreatedMessage message = new ModuleCreatedMessage(module);
            WeakReferenceMessenger.Default.Send(message);
        }

        private void OnSemesterCreated(SemesterCreatedMessage message)=> CanCreate = message.Value;
    }
}
