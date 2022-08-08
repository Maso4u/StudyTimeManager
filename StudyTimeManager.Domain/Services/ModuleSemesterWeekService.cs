using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using System.Globalization;

namespace StudyTimeManager.Domain.Services
{
    public class ModuleSemesterWeekService : IModuleSemesterWeekService
    {
        private readonly Semester _semester;

        public ModuleSemesterWeekService(Semester semester)
        {
            _semester = semester;
        }

        public void CreateModuleSemesterWeeks(string moduleCode)
        {
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;

            DateTime firstDateOfFirstWeek = _semester.StartDate.ToDateTime(TimeOnly.MinValue);
            int numberOfSemesterWeeks = _semester.NumberOfWeeks;

            for (int counter = 0; counter < numberOfSemesterWeeks; counter++)
            {
                DateTime firstDateOfWeek = calendar.AddWeeks(firstDateOfFirstWeek, counter);
                DateTime lastDateOfWeek = firstDateOfWeek.AddDays(6);

                ModuleSemesterWeek moduleSemesterWeek = new ModuleSemesterWeek()
                {
                    StartDate = DateOnly.FromDateTime(firstDateOfWeek),
                    EndDate = DateOnly.FromDateTime(lastDateOfWeek),
                    WeekNumber = counter + 1,
                    RemainingSelfStudyHours = _semester[moduleCode].RequiredWeeklySelfStudyHours
                };
                //_semester.Modules.First(m => m.Code.Equals(moduleCode)).Weeks.Add(moduleSemesterWeek);
                _semester[moduleCode].Weeks.Add(moduleSemesterWeek);
            }
        }

        public ICollection<ModuleSemesterWeek> GetModuleSemesterWeeksForAllModules(int week)
        {
            ICollection<ModuleSemesterWeek> moduleSemesterWeeksFound = new List<ModuleSemesterWeek>();
            foreach (var module in _semester.Modules)
            {
                moduleSemesterWeeksFound.Add(module[week]);
            }
            return moduleSemesterWeeksFound;
        }

        public bool UpdateSelfStudyHoursOfModuleSemesterWeek(string moduleCode, int week, int remainingHoursLeft)
        {
             int oldRemainingHours= _semester.Modules.First(m => m.Code.Equals(moduleCode))[week].RemainingSelfStudyHours;
            if (remainingHoursLeft<oldRemainingHours)
            {
                _semester.Modules.First(m => m.Code.Equals(moduleCode))[week].RemainingSelfStudyHours = remainingHoursLeft;
                return true;
            }
            return false;
        }
    }
}
