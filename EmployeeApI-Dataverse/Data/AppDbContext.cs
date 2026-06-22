using EmployeeApI_Dataverse.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApI_Dataverse.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
    }
}