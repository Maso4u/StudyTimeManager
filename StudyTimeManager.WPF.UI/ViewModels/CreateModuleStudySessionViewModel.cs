using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Shared.DTOs.Module;
using Shared.DTOs.Semester;
using Shared.DTOs.StudySession;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    /// <summary>
    /// Abstraction of the view responsible for the creation of a module
    /// </summary>
    public partial class CreateModuleStudySessionViewModel : ObservableObject
    {
        /// <summary>
        /// Observable collection of module listing view model items
        /// </summary>
        private readonly ObservableCollection<ModuleListingItemViewModel> _modules;

        /// <summary>
        /// Gets or sets the selected date for the study session
        /// </summary>
        [ObservableProperty]
        private DateTime _selectedDate;

        /// <summary>
        /// Gets or sets the the hours spent studying in the session
        /// </summary>
        [ObservableProperty]
        private int _hoursSpent;

        /// <summary>
        /// Gets or sets the module the study session is created for
        /// </summary>
        [ObservableProperty]
        private ModuleListingItemViewModel _selectedModuleListingItemViewModel;

        /// <summary>
        /// Gets or sets whether or not a study session can be created
        /// </summary>
        [ObservableProperty]
        private bool _canCreate;

        /// <summary>
        /// Gets or sets the start date of the semester
        /// </summary>
        [ObservableProperty]
        private DateTime _semesterStartDate;

        /// <summary>
        /// Gets or sets the end date of the semester
        /// </summary>
        [ObservableProperty]
        private DateTime _semesterEndDate;


        private bool _semesterExists = false;
        private bool _modulesExists = false;

        /// <summary>
        /// Gets the enumerable collection of module listing item viewmodels
        /// </summary>
        public IEnumerable<ModuleListingItemViewModel> Modules => _modules;
        private SemesterDTO? semester;
        private readonly IServiceManager _service;
        public ISnackbarMessageQueue MessageQueue { get; }
        public IAsyncRelayCommand AddStudySessionCommand { get; }

        public CreateModuleStudySessionViewModel(IServiceManager service,
            ISnackbarMessageQueue messageQueue)
        {
            _service = service;
            MessageQueue = messageQueue;
            _modules = new ObservableCollection<ModuleListingItemViewModel>();
            CanCreate = _semesterExists && _modulesExists;
            SelectedDate = SemesterStartDate;
            AddStudySessionCommand = new AsyncRelayCommand(AddStudySession);
            RegisterToMessages();
        }

        /// <summary>
        /// Command that creates a study session
        /// </summary>
        private async Task AddStudySession()
        {
            //instantiate a new study session
            StudySessionForCreationDTO studySession = new()
            {
                Date = SelectedDate,
                HoursSpent = HoursSpent
            };

            //try and create a study session
            StudySessionDTO? studySessionDTO = await _service.StudySessionService
                .CreateStudySession(SelectedModuleListingItemViewModel.Id, studySession);
            if (studySessionDTO is null)
            {
                MessageQueue.Enqueue(
                    $"Attempt to add a new study session for " +
                    $"{SelectedModuleListingItemViewModel.ModuleName}, " +
                    $"for the date {SelectedDate} was unsuccessful.");
                return;
            }

            StudySessionCreatedMessage message =
                new StudySessionCreatedMessage(SelectedModuleListingItemViewModel.Id);
            WeakReferenceMessenger.Default.Send(message);

            MessageQueue.Enqueue(
                "A new study session has been successfully added.",
                "UNDO",
                () => UndoStudySession(studySessionDTO));
        }

        /// <summary>
        /// Undoes the creation of a study session
        /// </summary>
        /// <param name="moduleCode">
        /// Code of the module for which a study session is to be undone for
        /// </param>
        /// <param name="week">
        /// The week number in the semester for which the study session was created on
        /// </param>
        private async Task UndoStudySession(StudySessionDTO studySession)
        {
            Guid moduleId = SelectedModuleListingItemViewModel.Id;
            await _service.StudySessionService.RemoveStudySession(moduleId, studySession);

            StudySessionRemovedMessage message = new(moduleId);
            WeakReferenceMessenger.Default.Send(message);
        }

        /// <summary>
        /// Registers this view model to messages that it should receive
        /// </summary>
        private void RegisterToMessages()
        {
            WeakReferenceMessenger.Default.Register<ModuleCreatedMessage>(this, (r, message) =>
            {
                _modulesExists = true;
                CanCreate = _semesterExists && _modulesExists;
                //add a new module listing item view model with the value of the message
                //(a ModuleDTO) as it argument
                _modules.Add(new ModuleListingItemViewModel(message.Value));
            });

            WeakReferenceMessenger.Default.Register<SemesterModulesFoundMessage>(this, (r, message) =>
            {
                //SelectedDate = SemesterStartDate;
                _modulesExists = true;
                CanCreate = _semesterExists && _modulesExists;
                foreach (var module in message.Value)
                {
                    _modules.Add(new ModuleListingItemViewModel(module));
                }

            });

            WeakReferenceMessenger.Default.Register<ModuleDeletedMessage>(this, (r, message) =>
            {
                RemoveModule(message.Value);
                if (Modules.Count() <= 0)
                {
                    CanCreate = false;
                }
            });
            WeakReferenceMessenger.Default.Register<CurrentSemesterSetMessage>(this, (r, message) =>
            {
                semester = message.Value;
                _semesterExists = true;
                CanCreate = _semesterExists && _modulesExists;
                //assign values to semester start and end date
                SemesterStartDate = semester.StartDate;
                SemesterEndDate = semester.EndDate;
                SelectedDate = SemesterStartDate;
            });

            WeakReferenceMessenger.Default.Register<SemesterDeletedMessage>(this, (r, m) =>
            {
                _semesterExists = false;
                _modulesExists = false;
                CanCreate = _semesterExists && _modulesExists;
                semester = null;
                _modules.Clear();
            });
        }

        /// <summary>
        /// Removes module listing item view model from the observable collection of them
        /// </summary>
        /// <param name="module">
        /// the module for which a module listing item view model must be removed
        /// </param>
        private void RemoveModule(ModuleDTO module)
        {
            //find the module listing item view model of
            //the module to be removed from observable collection
            ModuleListingItemViewModel? moduleListingFound = _modules
                .FirstOrDefault(m => m.Id == module.Id);
            if (moduleListingFound is null)
            {
                return;
            }
            //remove the found module listing item view model from
            //the observable collection it was found in
            _modules.Remove(moduleListingFound);
        }
    }
}
