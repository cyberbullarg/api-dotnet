using BasicAPI.Context.Persistence;
using BasicAPI.Interfaces;
using BasicAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BasicAPI.Repositories
{
    public class CategoryRepository(ApplicationContext context) : ICategoryRepository
    {
        private readonly ApplicationContext _context = context;

        public bool Create(Category category)
        {
            EntityEntry result = _context.Categories.Add(category);

            if (result.State != EntityState.Added)
            {
                return false;
            }

            _context.SaveChanges();

            return true;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.Include(c => c.Products).ToList();
        }

        public Category? GetById(Guid id)
        {
            return _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
        }
    }
}
