using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAllAsync(bool trackChanges);
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression,bool trackChanges);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
