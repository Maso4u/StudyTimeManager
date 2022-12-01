using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyTimeManager.Services.Contracts;

namespace StudyTimeManager.WebApp.UI.Pages.Forms
{
    public class DI_BasePageModel:PageModel
    {
        protected IServiceManager _services;
        //private readonly IAuthenticator _authenticator;
        public DI_BasePageModel(IServiceManager services)
        {
            _services = services;
        }
    }
}
