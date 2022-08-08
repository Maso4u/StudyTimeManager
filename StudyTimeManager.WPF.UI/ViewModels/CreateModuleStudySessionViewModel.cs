using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels;
public partial class CreateModuleStudySessionViewModel : ObservableValidator
{

    private readonly ObservableCollection<ModuleListingItemViewModel> _modules;
    
    [Required]
    [ObservableProperty]
    private DateTime _selectedDate;

    [Required]
    [ObservableProperty]
    private int hoursSpent;

    [ObservableProperty]
    private ModuleListingItemViewModel _selectedModuleListingItemViewModel;

    private readonly IServiceManager _service;

    public IEnumerable<ModuleListingItemViewModel> Modules => _modules;

    public ICommand AddStudySessionCommand { get; }

    public CreateModuleStudySessionViewModel(IServiceManager service)
    {
        _service = service;
        _modules = new ObservableCollection<ModuleListingItemViewModel>();
        _selectedDate = DateTime.Now;
        AddStudySessionCommand = new RelayCommand(Create);
        RegisterToModuleCreatedMessage();
    }

    [RelayCommand]
    private void Create()
    {

    }

    private void RegisterToModuleCreatedMessage()
    {
        WeakReferenceMessenger.Default.Register<ModuleCreatedMessage>(this,
            (_createModuleViewModel, message) =>
            {
                _modules.Add(new ModuleListingItemViewModel(message.Value));
            });
    }

}
