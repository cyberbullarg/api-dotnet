using System.Linq.Expressions;

namespace BasicAPI.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetOne(Expression<Func<T, bool>> predicate);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
