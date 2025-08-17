using Microsoft.EntityFrameworkCore;
using SpaERP.Models;

namespace SpaERP.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        // Add more DbSet<T> properties for other models as needed
    }
}