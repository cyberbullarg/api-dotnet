using BasicAPI.Model.Entities;

namespace BasicAPI.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category? GetById(Guid id);

        bool Create(Category category);
    }
}
