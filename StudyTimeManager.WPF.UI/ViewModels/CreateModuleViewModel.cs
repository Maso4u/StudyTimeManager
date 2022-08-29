using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    public partial class CreateModuleViewModel : ObservableValidator, 
        IRecipient<SemesterCreatedMessage>
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

        private Module module;

        private readonly IServiceManager _service;
        public ISnackbarMessageQueue MessageQueue { get; }
        public CreateModuleViewModel(IServiceManager service, ISnackbarMessageQueue messageQueue)
        {
            _service = service;
            MessageQueue = messageQueue;
            WeakReferenceMessenger.Default.Register<SemesterCreatedMessage>(this);
        }

        [RelayCommand]
        private void CreateModule()
        {
            module = new Module()
            {
                Code = _moduleCode,
                Name = _moduleName,
                NumberOfCredits = _numberOfCredits,
                ClassHoursPerWeek = _classHoursPerWeek
            };

            bool successful = _service.ModuleService.CreateModule(module);

            if (successful)
            {
                _service.ModuleSemesterWeekService.CreateModuleSemesterWeeks(ModuleCode);
                SendMessage(module);

                MessageQueue.Enqueue("Module successfully created.", "UNDO", () => UndoCreate());
            }

        }

        private void UndoCreate()
        {
            bool isDeleted = _service.ModuleService.DeleteModule(ModuleCode);

            if (isDeleted)
            {
                ModuleDeletedMessage message = new ModuleDeletedMessage(module);
                WeakReferenceMessenger.Default.Send(message);
            }
        }

        private void SendMessage(Module module)
        {
            ModuleCreatedMessage message = new ModuleCreatedMessage(module);
            WeakReferenceMessenger.Default.Send(message);
        }

        public void Receive(SemesterCreatedMessage message)
        {
            CanCreate = message.Value;
        }
    }
}
