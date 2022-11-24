using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.DTOs.Module;
using Shared.DTOs.Semester;
using StudyTimeManager.Services.Contracts;
using System.Collections;
using System.Collections.Generic;

namespace StudyTimeManager.WebApp.UI.Pages.Forms
{
    public class SemesterModulesModel : DI_BasePageModel
    {
        public SemesterModulesModel(IServiceManager service) : base(service)
        {
        }

        [BindProperty]
        public IList<ModuleDTO> Modules { get; set; }

        [BindProperty]
        public SemesterDTO Semester { get; set; }

        [BindProperty]
        public ModuleForCreationDTO Module { get; set; }

        public void OnGet()
        {
            //_service.SemesterService.CreateSemester()
        }
    }
}
