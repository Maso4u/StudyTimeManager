using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.DTOs.User;
using StudyTimeManager.Services.Contracts;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudyTimeManager.WebApp.UI.Pages.Forms
{
    public class AccountModel : DI_BasePageModel
    {
        [BindProperty]
        public UserForLoginDTO UserForLogin { get; set; }

        [BindProperty]
        public UserForRegisterationDTO UserForRegisteration { get; set; }

        public AccountModel(IServiceManager service) : base(service)
        {
        }

        public void OnGet()
        {
        }
        public async Task OnPostRegisterAsync()
        {
             RegisterationResult result = await _services.AuthenticationService.Register(UserForRegisteration.Username,
                UserForRegisteration.Password,
                UserForRegisteration.ConfirmPassword);

            switch (result)
            {
                case RegisterationResult.Success:
                    break;
                case RegisterationResult.PasswordsDoNotMatch:
                    break;
                case RegisterationResult.UsernameAlreadyExists:
                    break;
            }
        }
        public async Task<IActionResult> OnPostLoginAsync()
        {
            UserDTO? user = await _services.AuthenticationService
                .Login(UserForLogin.Username, UserForLogin.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim("Id", user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

            return Redirect("/SemesterModules");
        }
    }
}
