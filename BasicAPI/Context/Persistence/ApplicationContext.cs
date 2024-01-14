using BasicAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicAPI.Context.Persistence
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
