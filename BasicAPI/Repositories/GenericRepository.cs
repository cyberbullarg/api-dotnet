using BasicAPI.Context.Persistence;
using BasicAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace BasicAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        protected DbSet<T> EntitySet
        {
            get
            {
                return _context.Set<T>();
            }
        }

        public IEnumerable<T> GetAll()
        {
            // Buscamos en nuestra DB todos T
            return EntitySet.ToList();
        }

        public T? GetOne(Expression<Func<T, bool>> expression)
        {
            // Buscamos en nuestra DB el primer T en coincidir con la expresion recibida
            return EntitySet.AsNoTracking().FirstOrDefault(expression);
        }

        public bool Create(T entity)
        {
            // Hacemos el llamado al metodo Add de EF y le pasamos la entidad a crear
            EntityEntry result = EntitySet.Add(entity);

            // Verificamos el estado de la operacion, si no es Added, significa que ocurrio un problema y lo manejamos
            if (result.State != EntityState.Added)
            {
                return false;
            }

            // En este punto, si el estado de la operacion es el correcto, debemos guardar los cambios
            SaveChanges();

            // Finalmente retornamos true, indicando que se creo correctamente la entidad
            return true;
        }

        public bool Update(T entity)
        {
            // Hacemos el llamado al metodo Update de EF y le pasamos la entidad con sus campos actualizados
            EntityEntry result = EntitySet.Update(entity);

            // Verificamos el estado de la operacion, si no es Modified, significa que ocurrio un problema y lo manejamos
            if (result.State != EntityState.Modified)
            {
                return false;
            }

            // En este punto, si el estado de la operacion es el correcto, debemos guardar los cambios
            SaveChanges();

            // Finalmente retornamos true, indicando que se actualizo correctamente la entidad
            return true;
        }

        public bool Delete(T entity)
        {
            // Llamando al metodo Remove de EF pasandole la entidad a eliminar y almacenamos el resultado
            EntityEntry result = EntitySet.Remove(entity);

            // Verificamos el estado de la operacion, si no es Deleted, significa que ocurrio un problema y lo manejamos
            if (result.State != EntityState.Deleted)
            {
                return false;
            }

            // En este punto, si el estado de la operacion es el correcto, debemos guardar los cambios
            SaveChanges();

            // Finalmente retornamos true, indicando que se elimino correctamente la entidad
            return true;
        }

        private void SaveChanges() => _context.SaveChanges();
    }
}
