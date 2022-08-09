using CommunityToolkit.Mvvm.ComponentModel;
using StudyTimeManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimeManager.WPF.UI.ViewModels;
public class ModuleSemesterWeekListingItemViewModel 
{
    private readonly ModuleSemesterWeek _moduleSemesterWeek;

    public int WeekNumber => _moduleSemesterWeek.WeekNumber;
    public DateOnly StartDate => _moduleSemesterWeek.StartDate;
    public DateOnly EndDate => _moduleSemesterWeek.EndDate;
    public int RemainingSelfStudyHours => _moduleSemesterWeek.RemainingSelfStudyHours;

    public ModuleSemesterWeekListingItemViewModel(ModuleSemesterWeek moduleSemesterWeek)
    {
        _moduleSemesterWeek = moduleSemesterWeek;
    }
}
