using BasicAPI.Model.Entities;

namespace BasicAPI.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(Guid id);
        bool Create(Product product);
        bool Update(Product product);
        bool Delete(Product product);
    }
}
