using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels;
/// <summary>
/// Abstraction of the view responsible for listing a modules weeks in the semester
/// </summary>
public partial class ModuleSemesterWeekListingViewModel : ObservableObject, 
    IRecipient<StudySessionCreatedMessage>, 
    IRecipient<StudySessionRemovedMessage>,
    IRecipient<SelectedModuleListingItemViewModelChangedMessage>
{
    private readonly IServiceManager _service;
    /// <summary>
    /// Observable collection of module semester week listing items
    /// </summary>
    private readonly ObservableCollection<ModuleSemesterWeekListingItemViewModel> _moduleSemesterWeekListingItems;
   
    /// <summary>
    /// Gets the enumarable collection of semester week listing item viewmodels for a module
    /// </summary>
    public IEnumerable<ModuleSemesterWeekListingItemViewModel>
        ModuleSemesterWeekListingItemViewModel => _moduleSemesterWeekListingItems;

    /// <summary>
    /// Gets or sets the module listing item viewmodel currently selected in the moduleListingViewModel
    /// </summary>
    [ObservableProperty]
    private ModuleListingItemViewModel? _module;

    /// <summary>
    /// Gets or sets whether or not the module can be deleted
    /// </summary>
    [ObservableProperty]
    private bool _canDelete;

    public ModuleSemesterWeekListingViewModel(IServiceManager service)
    {
        _service = service;
        _moduleSemesterWeekListingItems = new ObservableCollection<ModuleSemesterWeekListingItemViewModel>();
        CanDelete = false;
        RegisterToMessages();
    }

    /// <summary>
    /// Deletes the module currently being 
    /// </summary>
    [RelayCommand]
    private void DeleteModule()
    {
        //delete module the module
        bool isDeleted = _service.ModuleService.DeleteModule(Module.ModuleCode);

        if (isDeleted)
        {
            //instantiate a new module with the values of the module with the properties 
            //equal to the listing item viewmodel properties
            Module module = new Module() {
                Code = Module.ModuleCode,
                Name = Module.ModuleName,
                NumberOfCredits = Module.NumberOfCredits,
                ClassHoursPerWeek = Module.ClassHoursPerWeek,
                RequiredWeeklySelfStudyHours = Module.RequiredWeeklyStudyHours
            };
            //create and send a module deleted module message 
            ModuleDeletedMessage message = new ModuleDeletedMessage(module);
            WeakReferenceMessenger.Default.Send(message);

            //clear the observable collection of module semester weeks
            //set value to module semester listing item viewmodel to null
            //set value of can delete to false
            _moduleSemesterWeekListingItems.Clear();
            Module = null;
            CanDelete = false;
        }
    }

    /// <summary>
    /// Updates the list of semester weeks for the module
    /// </summary>
    private void UpdateListing()
    {
        //clear the current list of semester week items
        _moduleSemesterWeekListingItems.Clear();
        if (Module != null)
        {
            //if module listing item viewModel is not null
            //(meaning there is a module listing item viewmodel currently selected
            //in the module listing viewmodel)

            //set can delete to true
            //and get semester weeks for the module with a code equal to
            //that of the module listing item viewmodel's code
            CanDelete = true;
            ICollection<ModuleSemesterWeek>? semesterWeeks = _service.ModuleSemesterWeekService
                .GetModuleSemesterWeeksForAModule(Module.ModuleCode);
            
            //loop through the retrieved semester weeks of the module 
            //and add semester week listing item viewmodel of the current semester week 
            foreach (var semesterWeek in semesterWeeks)
            {
                _moduleSemesterWeekListingItems
                    .Add(new ModuleSemesterWeekListingItemViewModel(semesterWeek));
            }
        }
    }

    /// <summary>
    /// Register module to messages it must receive
    /// </summary>
    public void RegisterToMessages()
    {
        WeakReferenceMessenger.Default
            .Register<SelectedModuleListingItemViewModelChangedMessage>(this);
        WeakReferenceMessenger.Default.Register<StudySessionCreatedMessage>(this);
        WeakReferenceMessenger.Default.Register<StudySessionRemovedMessage>(this);
    }

    public void Receive(SelectedModuleListingItemViewModelChangedMessage message)
    {
        //assign value of message to the module listing item viewmodel of this viewmodel
        //and update listing of semester week for this module
        Module = message.Value;
        UpdateListing();
    }

    public void Receive(StudySessionCreatedMessage message)
    {
        //if module listing item viewmodel is not null
        //and the code is equal to the value of the message 
        //then update the the listing 
        //(this means that the study session that has been created
        //has been created for the module selected in the module listing viewmodel)
        if (Module != null)
        {
            if (Module.ModuleCode.Equals(message.Value))
            {
                UpdateListing();
            }
        }
        
    }

    public void Receive(StudySessionRemovedMessage message)
    {
        if (Module != null)
        {
            if (Module.ModuleCode.Equals(message.Value))
            {
                UpdateListing();
            }
        }
    }
}

