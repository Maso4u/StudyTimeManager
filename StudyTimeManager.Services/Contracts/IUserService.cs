using Shared.DTOs.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyTimeManager.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDTO?> CreateUser(UserForRegisterationDTO user);
        Task<UserDTO?> UpdateUser(UserForRegisterationDTO user);   
        Task<bool> DeleteUser(Guid user);
        Task<UserDTO?> GetUser(string username, string password);
    }
}
