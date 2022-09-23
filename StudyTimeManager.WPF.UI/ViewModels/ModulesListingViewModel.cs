using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StudyTimeManager.WPF.UI.ViewModels;
/// <summary>
/// Abstraction of the view responsible for listing modules
/// </summary>
public partial class ModulesListingViewModel : ObservableObject,
    IRecipient<ModuleCreatedMessage>,
    IRecipient<ModuleDeletedMessage>
{
    private readonly IServiceManager _service;

    /// <summary>
    /// Observable collection of modules 
    /// </summary>
    private readonly ObservableCollection<ModuleListingItemViewModel> _modules;

    public IEnumerable<ModuleListingItemViewModel> Modules => _modules;

    private ModuleListingItemViewModel? _selectedModuleListingItemViewModel;

    public ModuleListingItemViewModel? SelectedModuleListingItemViewModel
    {
        get => _selectedModuleListingItemViewModel;
        set
        {
            SetProperty(ref _selectedModuleListingItemViewModel, value);
            SendSelectionChangedMessage(value);
        }
    }

    public ModulesListingViewModel(IServiceManager service)
    {
        _service = service;
        _modules = new ObservableCollection<ModuleListingItemViewModel>();
        RegisterToMessages();
    }

    public void RegisterToMessages()
    {
        WeakReferenceMessenger.Default.Register<ModuleCreatedMessage>(this);
        WeakReferenceMessenger.Default.Register<ModuleDeletedMessage>(this);
    }

    public void Receive(ModuleCreatedMessage message)
    {
        _modules.Add(new ModuleListingItemViewModel(message.Value));
    }
    public void Receive(ModuleDeletedMessage message)
    {
        ModuleListingItemViewModel module = _modules
            .First(m => m.Id.Equals(message.Value.Id));

        _modules.Remove(module);
    }

    private void SendSelectionChangedMessage(ModuleListingItemViewModel? selectedModuleListingViewModel)
    {
        SelectedModuleListingItemViewModelChangedMessage message =
                        new SelectedModuleListingItemViewModelChangedMessage(selectedModuleListingViewModel);

        WeakReferenceMessenger.Default.Send(message);
    }


}
