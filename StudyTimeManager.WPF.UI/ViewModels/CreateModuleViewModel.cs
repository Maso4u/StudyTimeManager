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

namespace StudyTimeManager.WPF.UI.ViewModels;
/// <summary>
/// Abstraction of the view responsible for the creation of a module
/// </summary>
public partial class CreateModuleViewModel : ObservableValidator,
        IRecipient<SemesterCreatedMessage>
{
    /// <summary>
    /// Code of the module
    /// </summary>
    [Required]
    [ObservableProperty]
    private string _moduleCode;

    /// <summary>
    /// Name of the module
    /// </summary>
    [Required]
    [ObservableProperty]
    private string _moduleName;

    /// <summary>
    /// Number of credits for the module
    /// </summary>
    [Required]
    [ObservableProperty]
    [Range(1, int.MaxValue)]
    private int _numberOfCredits;

    /// <summary>
    /// Number of class hours per week
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    [ObservableProperty]
    private int _classHoursPerWeek;

    /// <summary>
    /// Gets or Sets whether the module can be created or not.
    /// </summary>
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

    /// <summary>
    /// Command to creates a module
    /// </summary>
    [RelayCommand]
    private void CreateModule()
    {
        //instantiate a new module with the values of the properties and try to create it 
        module = new Module()
        {
            Code = ModuleCode,
            Name = ModuleName,
            NumberOfCredits = NumberOfCredits,
            ClassHoursPerWeek = ClassHoursPerWeek
        };
        bool successful = _service.ModuleService.CreateModule(module);

        //if the module was successfully created then create semester weeks for the module
        //and send message that it has been created. 
        //Add message to the message queue to notify users
        if (successful)
        {
            _service.ModuleSemesterWeekService.CreateModuleSemesterWeeks(ModuleCode);
            SendMessage(module);

            MessageQueue.Enqueue("Module successfully created.", "UNDO", () => UndoCreate());
        }

    }
    /// <summary>
    /// Reverts the creation of a module
    /// </summary>
    private void UndoCreate()
    {
        //delete the module
        bool isDeleted = _service.ModuleService.DeleteModule(ModuleCode);

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