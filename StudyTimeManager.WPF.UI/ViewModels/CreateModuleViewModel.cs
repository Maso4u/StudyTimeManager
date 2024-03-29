﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Shared.DTOs.Module;
using Shared.DTOs.Semester;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    /// <summary>
    /// Abstraction of the view responsible for the creation of a module
    /// </summary>
    public partial class CreateModuleViewModel : ObservableObject
    {
        /// <summary>
        /// Code of the module
        /// </summary>
        [ObservableProperty]
        private string _moduleCode;

        /// <summary>
        /// Name of the module
        /// </summary>
        [ObservableProperty]
        private string _moduleName;

        /// <summary>
        /// Number of credits for the module
        /// </summary>
        [ObservableProperty]
        private int _numberOfCredits;

        /// <summary>
        /// Number of class hours per week
        /// </summary>
        [ObservableProperty]
        private int _classHoursPerWeek;

        /// <summary>
        /// Gets or Sets whether the module can be created or not.
        /// </summary>
        [ObservableProperty]
        public bool _canCreate;

        private ModuleForCreationDTO module;
        private SemesterDTO? semester;

        private readonly IServiceManager _service;
        public ISnackbarMessageQueue MessageQueue { get; }
        public IAsyncRelayCommand CreateModuleCommand { get; }
        public CreateModuleViewModel(IServiceManager service, ISnackbarMessageQueue messageQueue)
        {
            _service = service;
            MessageQueue = messageQueue;
            CreateModuleCommand= new AsyncRelayCommand(CreateModule);
            RegisterToMessages();
        }

        /// <summary>
        /// Command to creates a module
        /// </summary>
        private async Task CreateModule()
        {
            //instantiate a new module with the values of the properties and try to create it 
            module = new ModuleForCreationDTO()
            {
                Code = ModuleCode,
                Name = ModuleName,
                NumberOfCredits = NumberOfCredits,
                ClassHoursPerWeek = ClassHoursPerWeek
            };
            bool moduleExists = _service.ModuleService.GetModule(semester.Id, ModuleCode) != null;

            if (moduleExists)
            {
                MessageQueue.Enqueue(
                    "This module has already been added for this semester.");
                return;
            }

            ModuleDTO? moduleDTO = await _service.ModuleService.CreateModule(semester, module, false);

            //if the module was successfully created then create semester weeks for the module
            //and send message that it has been created. 
            //Add message to the message queue to notify users
            if (moduleDTO is not null)
            {
                await _service.ModuleSemesterWeekService.CreateModuleSemesterWeeks(moduleDTO, semester);
                SendMessage(moduleDTO);

                MessageQueue.Enqueue(
                    "Module successfully created.",
                    "UNDO", () => UndoCreate(moduleDTO));
            }

        }
        /// <summary>
        /// Reverts the creation of a module
        /// </summary>
        private async Task UndoCreate(ModuleDTO? module)
        {
            //delete the module
            bool isDeleted = await _service.ModuleService.DeleteModule(semester.Id,module.Id);

            //if the module is successfully deleted then create
            //and send a message that deletion was successful
            if (isDeleted)
            {
                ModuleDeletedMessage message = new ModuleDeletedMessage(module);
                WeakReferenceMessenger.Default.Send(message);
            }
        }

        /// <summary>
        /// Sends a message on the creation of a module.
        /// </summary>
        /// <param name="module">Module to be created.</param>
        private void SendMessage(ModuleDTO module)
        {
            ModuleCreatedMessage message = new ModuleCreatedMessage(module);
            WeakReferenceMessenger.Default.Send(message);
        }

        private void RegisterToMessages()
        {
            WeakReferenceMessenger.Default.Register<CurrentSemesterSetMessage>(this, (r, m) =>
            {
                CanCreate = true;
                semester = m.Value;
            });

            WeakReferenceMessenger.Default.Register<SemesterDeletedMessage>(this, (r, m) =>
            {
                CanCreate = false;
                semester = null;
            });
        }
    }
}