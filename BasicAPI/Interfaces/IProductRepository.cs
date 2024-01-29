using BasicAPI.Model.Entities;

namespace BasicAPI.Interfaces
{
    public interface IProductRepository
    {
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments

        /// <summary>
        /// This method represents the search for all <c>Products</c>
        /// </summary>
        /// <returns>A collection of a <see cref="Product"/></returns>
        IEnumerable<Product> GetAll();

        /// <summary>
        /// This method represents the search for a <c>Product</c> by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="Product"/></returns>
        Product? GetById(Guid id);

        /// <summary>
        /// This method represents the creation of a Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>A <see cref="Boolean"/> indicating the result of the operation</returns>
        bool Create(Product product);

        /// <summary>
        /// This method represents the update of a Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>A <see cref="Boolean"/> indicating the result of the operation</returns>
        bool Update(Product product);

        /// <summary>
        /// This method represents the elimination of a Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>A <see cref="Boolean"/> indicating the result of the operation</returns>
        bool Delete(Product product);
    }
}
