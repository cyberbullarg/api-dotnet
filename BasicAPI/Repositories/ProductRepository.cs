using BasicAPI.Context.Persistence;
using BasicAPI.Interfaces;
using BasicAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BasicAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            // Buscamos en nuestra DB todos los productos existentes
            return _context.Products.ToList();
        }

        public Product? GetById(Guid id)
        {
            // Buscamos en nuestra DB el primer producto que coincida con el Id que recibimos
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public bool Create(Product product)
        {
            // Hacemos el llamado al metodo Add de EF y le pasamos nuestro objeto producto
            EntityEntry result = _context.Products.Add(product);

            // Verificamos el estado de la operacion, si no es Added, significa que ocurrio un problema y lo manejamos
            if (result.State != EntityState.Added)
            {
                return false;
            }

            // En este punto, si el estado de la operacion es el correcto, debemos guardar los cambios
            _context.SaveChanges();

            // Finalmente retornamos true, indicando que se creo correctamente el producto
            return true;
        }

        public bool Update(Product product)
        {
            // Hacemos el llamado al metodo Update de EF y le pasamos el producto sus campos actualizados
            EntityEntry result = _context.Products.Update(product);

            // Verificamos el estado de la operacion, si no es Modified, significa que ocurrio un problema y lo manejamos
            if (result.State != EntityState.Modified)
            {
                return false;
            }

            // En este punto, si el estado de la operacion es el correcto, debemos guardar los cambios
            _context.SaveChanges();

            // Finalmente retornamos true, indicando que se actualizo correctamente el producto
            return true;
        }

        public bool Delete(Product product)
        {
            // Llamando al metodo Remove de EF y almacenamos el resultado
            EntityEntry result = _context.Products.Remove(product);

            // Verificamos el estado de la operacion, si no es Deleted, significa que ocurrio un problema y lo manejamos
            if (result.State != EntityState.Deleted)
            {
                return false;
            }

            // En este punto, si el estado de la operacion es el correcto, debemos guardar los cambios
            _context.SaveChanges();

            // Finalmente retornamos true, indicando que se elimino correctamente el producto
            return true;
        }
    }
}
