using BasicAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicAPI.Context.Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
