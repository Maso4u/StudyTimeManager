using AutoMapper;
using Shared.DTOs.Semester;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace StudyTimeManager.Services
{

    /// <inheritdoc cref="ISemesterService"/>
    internal sealed class SemesterService : ISemesterService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public SemesterService(
            IRepositoryManager repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SemesterDTO?> CreateSemester(Guid studentId,SemesterForCreationDTO semester)
        {
            if (semester is null)
            {
                return null;
            }

            ///validate whether or not the number of weeks of semester parameter value are <= 0.
            if (semester.NumberOfWeeks <= 0)
            {
                return null;
            }

            ///validate whether or not start date of semester parameter value is null or empty.
            if (String.IsNullOrEmpty(semester.StartDate.ToString()))
            {
                return null;
            }

            var semesterEntity = _mapper.Map<Semester>(semester);
            semesterEntity.UserId = studentId;
            semesterEntity.EndDate = CalculateSemesterLastDay(semester.StartDate, semester.NumberOfWeeks);
            await _repository.Semester.CreateSemester(semesterEntity);

            var semesterToReturn = _mapper.Map<SemesterDTO>(semesterEntity);

            return semesterToReturn;
        }

        public async Task<bool> DeleteSemester(Guid Id)
        {
            Semester? semester = await _repository.Semester.GetSemester(Id, false);
            if (semester is null)
            {
                return false;
            }
            await _repository.Semester.DeleteSemester(semester);
            return true;
        }

        public async Task<SemesterDTO?> GetSemester(Guid UserId, bool trackChanges)
        {
            Semester? semester = await _repository.Semester.GetSemesterByUser(UserId, trackChanges);
            if (semester is null)
            {
                return null;
            }
            return _mapper.Map<SemesterDTO>(semester);
        }

        /// <summary>
        /// Calculates the last day of the semester and returns it as type DateOnly
        /// </summary>
        /// <remarks>
        /// This is done based on <paramref name="startDate"/> and 
        /// <paramref name="numberOfWeeks"/> for the semester
        /// </remarks>
        /// <param name="startDate">The start date of the semester</param>
        /// <param name="numberOfWeeks">The number of weeks in the semester</param>
        /// <returns>A DateOnly</returns>
        private DateTime CalculateSemesterLastDay(DateTime startDate, int numberOfWeeks)
        {

            Calendar calendar = CultureInfo.InvariantCulture.Calendar;

            ///determine date that is the number of the semester's weeks away 
            ///and gets date before then to determine last day of semester
            DateTime lastDate = calendar
                .AddWeeks(startDate, numberOfWeeks)
                .AddDays(-1);

            return lastDate;
        }
    }
}