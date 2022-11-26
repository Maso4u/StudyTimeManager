using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyTimeManager.Services.Contracts;

namespace StudyTimeManager.WebApp.UI.Pages.Forms
{
    public class SemesterModulesModel : DI_BasePageModel
    {
        public SemesterModulesModel(IServiceManager service) : base(service)
        {
        }

        public void OnGet()
        {
        }
    }
}
