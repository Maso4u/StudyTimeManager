using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels;
public partial class CreateModuleStudySessionViewModel : ObservableRecipient
{

    private readonly ObservableCollection<ModuleListingItemViewModel> _modules;

    [Required]
    private DateOnly _selectedDate;

    [Required]
    private int hoursSpent;

    private readonly IServiceManager _service;

    public ICommand AddStudySessionCommand { get; }

    public CreateModuleStudySessionViewModel(IServiceManager service)
    {
        _service = service;
        _modules = new ObservableCollection<ModuleListingItemViewModel>();
        _selectedDate = DateOnly.FromDateTime(DateTime.Now);
    }
}
