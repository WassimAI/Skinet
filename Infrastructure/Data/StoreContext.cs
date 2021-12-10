using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        //the base here takes the base constructor from the parent class which is the DbContext (you can see it in the definitions methods)
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}