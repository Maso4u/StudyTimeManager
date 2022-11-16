using Shared.DTOs.User;
using StudyTimeManager.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace StudyTimeManager.WPF.UI.State.Authenticators
{
    public interface IAuthenticator
    {
        UserDTO CurrentUser { get; }
        bool IsLoggedIn { get; }
        Task<bool> Login(UserForLoginDTO loginDTO);
        Task<RegisterationResult> Register(UserForRegisterationDTO registerationDTO);
        
    }
}