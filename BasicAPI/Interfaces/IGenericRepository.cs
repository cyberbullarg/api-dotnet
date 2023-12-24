using BasicAPI.Shared;
using System.Linq.Expressions;

namespace BasicAPI.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetOne(Expression<Func<T, bool>> predicate);
        Result Create(T entity);
        Result Update(T entity);
        Result Delete(T entity);
    }
}
