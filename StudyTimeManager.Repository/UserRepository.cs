using Repository;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public UserRepository(RepositoryContextFactory repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateUser(User user)
        {
            await CreateAsync(user);
        }

        public async Task DeleteUser(User user)
        {
            await DeleteAsync(user);
        }

        public async Task<User?> GetUser(Guid id)
        {
            var result = await FindByConditionAsync(u => u.Id.Equals(id), false);
            return result.SingleOrDefault();
        }

        public async Task<User?> GetUser(string username)
        {
            var result = await FindByConditionAsync(u => u.Username.Equals(username), false);
            return result.SingleOrDefault();
        }

        public async Task UpdateUser(User user)
        {
            await UpdateAsync(user);
        }
    }
}
