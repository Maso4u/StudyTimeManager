using StudyTimeManager.Domain.Models;
using System;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        User GetUser(Guid id);
        User GetUser(string username);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
