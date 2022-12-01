using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Semester;
using StudyTimeManager.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace StudyTimeManager.WebApp.UI.Pages.Forms
{
    [Authorize]
    public class SemesterModulesModel : DI_BasePageModel
    {
        [BindProperty]
        public bool UserHasSemester { get; set; }

        [BindProperty]
        public SemesterDTO? UserSemester { get; set; } = new SemesterDTO();
        public SemesterModulesModel(IServiceManager services) : base(services)
        {
        }

        public async Task OnGet()
        {
            UserSemester = await _services.SemesterService
                            .GetSemester(new Guid(User.FindFirst("Id").Value), false);
            UserHasSemester = UserSemester is not null;
        }

        public async Task<JsonResult> OnGetSemester()
        {
            UserSemester = await _services.SemesterService
                .GetSemester(new Guid(User.FindFirst("Id").Value), false);
            UserHasSemester = UserSemester is not null;

            return new JsonResult(UserSemester);
        }

        public async Task<JsonResult> OnPostCreateSemester(string startDate, int numberOfWeeks)
        {
            if (string.IsNullOrEmpty(startDate) && numberOfWeeks <= 0)
            {
                return new JsonResult(null);
            }
            SemesterForCreationDTO semesterForCreationDTO = new SemesterForCreationDTO()
            {
                StartDate = DateTime.ParseExact(startDate, "dd/MM/yyyy", null),
                NumberOfWeeks = numberOfWeeks
            };

            UserSemester = await _services.SemesterService
                .CreateSemester(new Guid(User.FindFirst("Id").Value), semesterForCreationDTO);
            UserHasSemester = UserSemester is not null;

            return UserHasSemester ? new JsonResult(UserSemester) : new JsonResult(null);
        }

        public async Task<JsonResult> OnPostDeleteSemester(Guid semesterId)
        {
            UserHasSemester = !await _services.SemesterService.DeleteSemester(semesterId);
            return new JsonResult(UserHasSemester.ToString());
        }

    }
}