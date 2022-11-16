using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Shared.DTOs.Module;
using Shared.DTOs.ModuleSemesterWeek;
using Shared.DTOs.Semester;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    /// <summary>
    /// Abstraction of the view responsible for listing a modules weeks in the semester
    /// </summary>
    public partial class ModuleSemesterWeekListingViewModel : ObservableObject
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

        private SemesterDTO semester;

        public IAsyncRelayCommand DeleteModuleCommand { get; set; }

        public ModuleSemesterWeekListingViewModel(IServiceManager service)
        {
            _service = service;
            _moduleSemesterWeekListingItems =
                new ObservableCollection<ModuleSemesterWeekListingItemViewModel>();
            CanDelete = false;
            DeleteModuleCommand = new AsyncRelayCommand(DeleteModule);
            RegisterToMessages();
        }

        /// <summary>
        /// Deletes the module currently being 
        /// </summary>
        private async Task DeleteModule()
        {
            //delete module the module
            bool isDeleted = await _service.ModuleService.DeleteModule(semester.Id, Module.Id);

            if (isDeleted)
            {
                //instantiate a new module with the values of the module with the properties 
                //equal to the listing item viewmodel properties
                ModuleDTO module = new ModuleDTO()
                {
                    Id = Module.Id,
                    Code = Module.ModuleCode,
                    Name = Module.ModuleName,
                    NumberOfCredits = Module.NumberOfCredits,
                    ClassHoursPerWeek = Module.ClassHoursPerWeek,
                    RequiredWeeklySelfStudyHours = Module.RequiredWeeklyStudyHours
                };
                //create and send a module deleted module message 
                ModuleDeletedMessage message = new ModuleDeletedMessage(module);
                WeakReferenceMessenger.Default.Send(message);

                Module = null;
                UpdateListing();
            }
        }

        /// <summary>
        /// Updates the list of semester weeks for the module
        /// </summary>
        private async Task UpdateListing()
        {
            //clear the current list of semester week items
            _moduleSemesterWeekListingItems.Clear();
            if (Module is null)
            {
                //if module listing item viewModel is not null
                //(meaning there is a module listing item viewmodel currently selected
                //in the module listing viewmodel)

                //set can delete to true
                //and get semester weeks for the module with a code equal to
                //that of the module listing item viewmodel's code
                CanDelete = false;
                return;
            }

            CanDelete = true;
            IEnumerable<ModuleSemesterWeekDTO>? semesterWeeks = await _service.ModuleSemesterWeekService
                .GetModuleSemesterWeeksForAModule(Module.Id);

            if (semesterWeeks is null)
            {
                return;
            }

            foreach (var semesterWeek in semesterWeeks)
            {
                _moduleSemesterWeekListingItems
                    .Add(new ModuleSemesterWeekListingItemViewModel(semesterWeek));
            }
        }

        /// <summary>
        /// Register module to messages it must receive
        /// </summary>
        private void RegisterToMessages()
        {
            WeakReferenceMessenger.Default.Register<CurrentSemesterSetMessage>(this, (r, m) =>
            {
                semester = m.Value;
            });

            WeakReferenceMessenger.Default.Register<SemesterDeletedMessage>(this, (r, message) =>
            {
                Module = null;
                UpdateListing();
                CanDelete = false;
            });

            WeakReferenceMessenger.Default
                .Register<SelectedModuleListingItemViewModelChangedMessage>(this, (r, message) =>
                {
                    //assign value of message to the module listing item viewmodel of this viewmodel
                    //and update listing of semester week for this module
                    Module = message.Value;
                    UpdateListing();
                });

            WeakReferenceMessenger.Default.Register<StudySessionCreatedMessage>(this, (r, message) =>
            {
                //_service.ModuleSemesterWeekService.UpdateModuleSemesterWeekForAModule(message.Value);
                if (Module is null)
                {
                    return;
                }

                if (Module.Id.Equals(message.Value))
                {
                    UpdateListing();
                }
            });

            WeakReferenceMessenger.Default.Register<StudySessionRemovedMessage>(this, (r, message) =>
            {
                if (Module is null)
                {
                    return;
                }
                if (Module.Id.Equals(message.Value))
                {
                    UpdateListing();
                }
            });
        }
    }
}