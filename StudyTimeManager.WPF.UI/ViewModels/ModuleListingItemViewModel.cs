using Shared.DTOs.Module;
using StudyTimeManager.Domain.Models;
using System;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    /// <summary>
    /// Abstraction of an item to be displayed in a listing of modules
    /// </summary>
    public class ModuleListingItemViewModel
    {
        /// <summary>
        /// The module item to be listed
        /// </summary>
        private readonly ModuleDTO _module;
        public Guid Id => _module.Id;
        public string? ModuleCode => _module?.Code;
        public string? ModuleName => _module.Name;
        public int NumberOfCredits => _module.NumberOfCredits;
        public int ClassHoursPerWeek => _module.ClassHoursPerWeek;
        public int RequiredWeeklyStudyHours => _module.RequiredWeeklySelfStudyHours;
        public ModuleListingItemViewModel(ModuleDTO module)
        {
            _module = module;
        }
    }
}