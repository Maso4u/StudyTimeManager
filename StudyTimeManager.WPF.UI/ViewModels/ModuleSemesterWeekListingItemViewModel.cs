using CommunityToolkit.Mvvm.ComponentModel;
using Shared.DTOs.ModuleSemesterWeek;
using StudyTimeManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimeManager.WPF.UI.ViewModels;
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
    public DateOnly? StartDate => _moduleSemesterWeek.StartDate;
    public DateOnly? EndDate => _moduleSemesterWeek.EndDate;
    public int RemainingSelfStudyHours => _moduleSemesterWeek.RemainingSelfStudyHours;

    public ModuleSemesterWeekListingItemViewModel(ModuleSemesterWeekDTO moduleSemesterWeek)
    {
        _moduleSemesterWeek = moduleSemesterWeek;
    }
}
