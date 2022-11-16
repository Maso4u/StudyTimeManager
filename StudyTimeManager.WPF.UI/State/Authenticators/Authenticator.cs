using Shared.DTOs.User;
using StudyTimeManager.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace StudyTimeManager.WPF.UI.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        public UserDTO? CurrentUser { get; set; }
        public bool IsLoggedIn => CurrentUser is not null;

        private readonly IServiceManager _serviceManager;

        public Authenticator(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<RegisterationResult> Register(UserForRegisterationDTO registerationDTO)
        {
            return await _serviceManager.AuthenticationService.Register(registerationDTO.Username,
                registerationDTO.Password, registerationDTO.ConfirmPassword);

        }

        public async Task<bool> Login(UserForLoginDTO loginDTO)
        {

            try
            {
                CurrentUser = await _serviceManager.AuthenticationService.Login(loginDTO.Username, loginDTO.Password);
               
                return CurrentUser is not null;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

    }
}
