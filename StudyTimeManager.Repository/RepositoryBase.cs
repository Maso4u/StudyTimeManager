using Microsoft.EntityFrameworkCore;
using StudyTimeManager.Repository;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Repository.ContextFactory;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContextFactory _repositoryContextFactory;

        protected RepositoryBase(RepositoryContextFactory repositoryContext)
        {
            _repositoryContextFactory = repositoryContext;
        }

        public void Create(T entity)
        {
            using (RepositoryContext context = _repositoryContextFactory.CreateDbContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (RepositoryContext context = _repositoryContextFactory.CreateDbContext())
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        public IEnumerable<T> FindAll(bool trackChanges)
        {
            using (RepositoryContext context = _repositoryContextFactory.CreateDbContext())
            {
                if (trackChanges)
                {
                    return context.Set<T>().AsNoTracking().ToList();
                }
                return context.Set<T>().ToList();
            }
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            using (RepositoryContext context = _repositoryContextFactory.CreateDbContext())
            {
                if (trackChanges)
                {
                    return context.Set<T>().Where(expression).ToList(); ;
                }
                var response =context.Set<T>().Where(expression).AsNoTracking();
                return response.ToList();
            }
        }

        public void Update(T entity)
        {
            using (RepositoryContext context = _repositoryContextFactory.CreateDbContext())
            {
                context.Set<T>().Update(entity);
                context.SaveChanges();
            }
        }
    }
}
