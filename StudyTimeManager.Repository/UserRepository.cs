using Repository;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using System;
using System.Linq;

namespace StudyTimeManager.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public UserRepository(RepositoryContextFactory repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public User? GetUser(Guid id)
        {
            return FindByCondition(u => u.Id.Equals(id),false)
                .SingleOrDefault();
        }

        public User? GetUser(string username)
        {
            return FindByCondition(u => u.Username.Equals(username), false).SingleOrDefault();
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }
    }
}
