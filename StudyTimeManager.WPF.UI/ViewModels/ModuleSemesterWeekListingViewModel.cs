using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels;
public partial class ModuleSemesterWeekListingViewModel : ObservableObject
{
    private readonly IServiceManager _service;
    private readonly ObservableCollection<ModuleSemesterWeekListingItemViewModel> _moduleSemesterListingItems;

    public IEnumerable<ModuleSemesterWeekListingItemViewModel>
        ModuleSemesterWeekListingItemViewModel => _moduleSemesterListingItems;

    [ObservableProperty]
    private ModuleListingItemViewModel? _module;

    [ObservableProperty]
    private bool _canDelete;

    public ICommand DeleteModuleCommand { get; }


    public ModuleSemesterWeekListingViewModel(IServiceManager service)
    {
        _service = service;
        _moduleSemesterListingItems = new ObservableCollection<ModuleSemesterWeekListingItemViewModel>();
        CanDelete = false;
        RegisterToModuleSelectionChangeMessage();
        DeleteModuleCommand = new RelayCommand(Delete);
    }

    private void Delete()
    {
        bool isDeleted = _service.ModuleService.DeleteModule(Module.ModuleCode);
        if (isDeleted)
        {
            ModuleDeletedMessage message = new ModuleDeletedMessage(Module);
            WeakReferenceMessenger.Default.Send(message);

            _moduleSemesterListingItems.Clear();
            Module = null;
            CanDelete = false;
        }
    }

    public void RegisterToModuleSelectionChangeMessage()
    {
        WeakReferenceMessenger.Default
            .Register<SelectedModuleListingItemViewModelChangedMessage>(this,
            (_moduleSemesterWeekListingViewModel, message) =>
            {
                Module = message.Value;
                UpdateListing();
            });
    }

    private void UpdateListing()
    {
        _moduleSemesterListingItems.Clear();
        if (Module != null)
        {
            CanDelete = true;
            ICollection<ModuleSemesterWeek> semesterWeeks = _service.ModuleSemesterWeekService
                .GetModuleSemesterWeeksForAModule(Module.ModuleCode);

            foreach (var semesterWeek in semesterWeeks)
            {
                _moduleSemesterListingItems
                    .Add(new ModuleSemesterWeekListingItemViewModel(semesterWeek));
            }
        }
    }

}

