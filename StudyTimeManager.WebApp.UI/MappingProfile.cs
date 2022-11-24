using AutoMapper;
using Shared.DTOs.Module;
using Shared.DTOs.ModuleSemesterWeek;
using Shared.DTOs.Semester;
using Shared.DTOs.StudySession;
using Shared.DTOs.User;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.WebApp.UI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //user maps
            CreateMap<User, UserForRegisterationDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            //semester maps
            CreateMap<Semester, SemesterForCreationDTO>().ReverseMap();
            CreateMap<Semester, SemesterDTO>().ReverseMap();

            //module maps
            CreateMap<Module, ModuleForCreationDTO>().ReverseMap();
            CreateMap<Module, ModuleDTO>().ReverseMap();

            //module semester week
            CreateMap<ModuleSemesterWeek, ModuleSemesterForCreationDTO>().ReverseMap();
            CreateMap<ModuleSemesterForUpdateDTO, ModuleSemesterWeek>().ReverseMap();
            CreateMap<ModuleSemesterWeekDTO, ModuleSemesterWeek>().ReverseMap();

            //studySession 
            CreateMap<StudySession, StudySessionDTO>().ReverseMap();
            CreateMap<StudySession, StudySessionForCreationDTO>().ReverseMap();
        }
    }
}
