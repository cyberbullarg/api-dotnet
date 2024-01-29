using BasicAPI.Context.Persistence;
using BasicAPI.Interfaces;
using BasicAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BasicAPI.Repositories
{
    public class ProductRepository(ApplicationContext context) : IProductRepository
    {
        private readonly ApplicationContext _context = context;

        /// <summary>
        /// This method implements the search for all <c>products</c>
        /// </summary>
        /// <returns>A list of <see cref="Product"/></returns>
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        /// <summary>
        /// This method implements the search for a <c>Product</c> by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="Product"/></returns>
        public Product? GetById(Guid id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// This method implements the creation of a Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>A <see cref="Boolean"/> indicating the result of the operation</returns>
        public bool Create(Product product)
        {
            // Hacemos el llamado al metodo Add de EF y le pasamos nuestro objeto producto
            EntityEntry result = _context.Products.Add(product);

            // Verificamos el estado de la operacion, si no es 'Added', significa que ocurrio un problema y lo manejamos
            if (result.State != EntityState.Added)
            {
                return false;
            }

            // En este punto, si el estado de la operacion es el correcto, debemos guardar los cambios
            _context.SaveChanges();

            // Finalmente retornamos true, indicando que se creo correctamente el producto
            return true;
        }

        /// <summary>
        /// This method implements the update of a Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>A <see cref="Boolean"/> indicating the result of the operation</returns>
        public bool Update(Product product)
        {
            // Hacemos el llamado al metodo Update de EF y le pasamos el producto sus campos actualizados
            EntityEntry result = _context.Products.Update(product);

            // Verificamos el estado de la operacion, si no es 'Modified', significa que ocurrio un problema y lo manejamos
            if (result.State != EntityState.Modified)
            {
                return false;
            }

            // En este punto, si el estado de la operacion es el correcto, debemos guardar los cambios
            _context.SaveChanges();

            // Finalmente retornamos true, indicando que se actualizo correctamente el producto
            return true;
        }

        /// <summary>
        /// This method implements the elimination of a Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>A <see cref="Boolean"/> indicating the result of the operation</returns>
        public bool Delete(Product product)
        {
            // Llamando al metodo Remove de EF y almacenamos el resultado
            EntityEntry result = _context.Products.Remove(product);

            // Verificamos el estado de la operacion, si no es 'Deleted', significa que ocurrio un problema y lo manejamos
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
