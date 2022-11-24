using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.DTOs.User;

namespace StudyTimeManager.WebApp.UI.Pages.Forms
{
    public class AccountModel : PageModel
    {
        [BindProperty]
        public UserForLoginDTO UserForLogin { get; set; }
        [BindProperty]
        public UserForRegisterationDTO UserForRegisteration { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPostRegister()
        {
            return Redirect("");
        }
        public IActionResult OnPostLogin()
        {

            return Redirect("");
        }
    }
}
