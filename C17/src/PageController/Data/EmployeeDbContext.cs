using Microsoft.EntityFrameworkCore;
using PageController.Data.Models;

namespace PageController.Data;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions options)
        : base(options)
    {
    }
    public DbSet<Employee> Employees => Set<Employee>();
}
