using StudyTimeManager.Domain.Models;
using System;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<User?> GetUser(Guid id);
        Task<User?> GetUser(string username);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
    }
}
