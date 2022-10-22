using Shared.DTOs.User;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Services.Contracts;

namespace StudyTimeManager.WPF.UI.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        public UserDTO CurrentUser { get; set; }
        public bool IsLoggedIn => CurrentUser is not null;

        private readonly IServiceManager _serviceManager;

        public Authenticator(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public RegisterationResult Register(UserForRegisterationDTO registerationDTO)
        {
            return _serviceManager.AuthenticationService.Register(registerationDTO.Username,
                registerationDTO.Password, registerationDTO.ConfirmPassword);

        }

        public bool Login(UserForLoginDTO loginDTO)
        {

            try
            {
                CurrentUser = _serviceManager.AuthenticationService.Login(loginDTO.Username, loginDTO.Password);
                return CurrentUser is not null;
            }
            catch (System.Exception)
            {
                return false;
            }
            
        }
    }
}
