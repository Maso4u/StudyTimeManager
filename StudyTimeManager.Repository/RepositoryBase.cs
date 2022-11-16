using Microsoft.EntityFrameworkCore;
using StudyTimeManager.Repository;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Repository.ContextFactory;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContextFactory _repositoryContextFactory;

        protected RepositoryBase(RepositoryContextFactory repositoryContext)
        {
            _repositoryContextFactory = repositoryContext;
        }

        public async Task CreateAsync(T entity)
        {
            using (RepositoryContext context = _repositoryContextFactory.CreateDbContext())
            {
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            using (RepositoryContext context = _repositoryContextFactory.CreateDbContext())
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> FindAllAsync(bool trackChanges)
        {
            using (RepositoryContext context = _repositoryContextFactory.CreateDbContext())
            {
                if (trackChanges)
                {
                    return await context.Set<T>().AsNoTracking().ToListAsync();
                }
                return await context.Set<T>().ToListAsync();
            }
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            using (RepositoryContext context = _repositoryContextFactory.CreateDbContext())
            {
                if (trackChanges)
                {
                    return await context.Set<T>().Where(expression).ToListAsync(); ;
                }
                var response = context.Set<T>().Where(expression).AsNoTracking();
                return await response.ToListAsync();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            using (RepositoryContext context = _repositoryContextFactory.CreateDbContext())
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
