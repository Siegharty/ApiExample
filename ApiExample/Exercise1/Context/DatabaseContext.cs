using Exercise1.Entity;
using Microsoft.EntityFrameworkCore;

namespace Exercise1.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
               : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }


}
