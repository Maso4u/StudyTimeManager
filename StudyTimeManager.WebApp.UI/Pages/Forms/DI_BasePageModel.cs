using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyTimeManager.Services.Contracts;

namespace StudyTimeManager.WebApp.UI.Pages.Forms
{
    public class DI_BasePageModel:PageModel
    {
        protected IServiceManager _service;
        //private readonly IAuthenticator _authenticator;
        public DI_BasePageModel(IServiceManager service)
        {
            _service = service;
        }
    }
}
