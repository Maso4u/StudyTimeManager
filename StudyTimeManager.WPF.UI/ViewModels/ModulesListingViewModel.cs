using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace StudyTimeManager.WPF.UI.ViewModels;
public partial class ModulesListingViewModel : ObservableObject
{
    private readonly IServiceManager _service;
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
        RegisterToModuleCreatedMessage();
        RegisterToModuleDeletedMessage();
    }

    public void RegisterToModuleCreatedMessage()
    {
        WeakReferenceMessenger.Default.Register<ModuleCreatedMessage>(this,
            (_createModuleViewModel, message) =>
                {
                    _modules.Add(new ModuleListingItemViewModel(message.Value));
                });
    }
    public void RegisterToModuleDeletedMessage()
    {
        WeakReferenceMessenger.Default.Register<ModuleDeletedMessage>(this,
            (_createModuleViewModel, message) =>
                {
                    UpdateListing();
                });
    }

    private void SendSelectionChangedMessage(ModuleListingItemViewModel? selectedModuleListingViewModel)
    {
        SelectedModuleListingItemViewModelChangedMessage message =
                        new SelectedModuleListingItemViewModelChangedMessage(selectedModuleListingViewModel);

        WeakReferenceMessenger.Default.Send(message);
    }

    private void UpdateListing()
    {
        _modules.Remove(SelectedModuleListingItemViewModel);
    }

}
