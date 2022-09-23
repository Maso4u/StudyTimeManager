using AutoMapper;
using Shared.DTOs.Module;
using Shared.DTOs.ModuleSemesterWeek;
using Shared.DTOs.Semester;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System.Collections.ObjectModel;
using System.Globalization;

namespace StudyTimeManager.Services
{
    ///<inheritdoc cref="IModuleSemesterWeekService"/>
    public class ModuleSemesterWeekService : IModuleSemesterWeekService
    {
        private readonly Semester _semester;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ModuleSemesterWeekService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repository = repositoryManager;
            _mapper = mapper;
        }

        public void CreateModuleSemesterWeeks(ModuleDTO module, SemesterDTO semester)
        {
            List<ModuleSemesterWeek> moduleSemesterWeeks = new List<ModuleSemesterWeek>();
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            DateTime firstDateOfFirstWeek = semester.StartDate.ToDateTime(TimeOnly.MinValue);
            int week = 0;

            while (week < semester.NumberOfWeeks)
            {
                //determine the first and last date of a week
                DateTime firstDateOfWeek = calendar.AddWeeks(firstDateOfFirstWeek, week);
                DateTime lastDateOfWeek = firstDateOfWeek.AddDays(6);

                ModuleSemesterWeek moduleSemesterWeek = new()
                {
                    StartDate = DateOnly.FromDateTime(firstDateOfWeek),
                    EndDate = DateOnly.FromDateTime(lastDateOfWeek),
                    WeekNumber = week+1,
                    RemainingSelfStudyHours = module.RequiredWeeklySelfStudyHours,
                    ModuleId = module.Id
                };

                moduleSemesterWeeks.Add(moduleSemesterWeek);
                week++;
            }
            _repository.ModuleSemesterWeek.CreateModuleSemesterWeeks(moduleSemesterWeeks);
            _repository.Save();
        }

        public IEnumerable<ModuleSemesterWeekDTO>? GetModuleSemesterWeeksForAModule(Guid moduleId)
        {
            IEnumerable<ModuleSemesterWeek> moduleSemesterWeeks = _repository.ModuleSemesterWeek
                    .GetModuleSemesterWeeksForAModule(moduleId, false);

            IEnumerable<ModuleSemesterWeekDTO> moduleSemesterWeeksDTO = _mapper
                .Map<IEnumerable<ModuleSemesterWeekDTO>>(moduleSemesterWeeks);

            return moduleSemesterWeeksDTO;
        }

    }
}
