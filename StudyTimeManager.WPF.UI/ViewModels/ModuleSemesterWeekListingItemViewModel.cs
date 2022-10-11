using Shared.DTOs.ModuleSemesterWeek;
using System;

namespace StudyTimeManager.WPF.UI.ViewModels
{
    /// <summary>
    /// Abstraction of an semester week to be displayed in a listing of semester week for a module
    /// </summary>
    public class ModuleSemesterWeekListingItemViewModel
    {
        /// <summary>
        /// The semester week of a module to be listed
        /// </summary>
        private readonly ModuleSemesterWeekDTO _moduleSemesterWeek;

        public int WeekNumber => _moduleSemesterWeek.WeekNumber;
        public string? StartDate => _moduleSemesterWeek.StartDate.ToShortDateString();
        public string? EndDate => _moduleSemesterWeek.EndDate.ToShortDateString();
        public int RemainingSelfStudyHours => _moduleSemesterWeek.RemainingSelfStudyHours;

        public ModuleSemesterWeekListingItemViewModel(ModuleSemesterWeekDTO moduleSemesterWeek)
        {
            _moduleSemesterWeek = moduleSemesterWeek;
        }
    }
}