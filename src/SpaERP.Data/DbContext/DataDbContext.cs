using Microsoft.EntityFrameworkCore;
using SpaERP.Models;
using SpaERP.Models.Models;

namespace SpaERP.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

        // Add more DbSet<T> properties for other models as needed
    }
}