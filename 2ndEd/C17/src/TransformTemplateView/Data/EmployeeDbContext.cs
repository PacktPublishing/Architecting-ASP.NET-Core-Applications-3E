using Microsoft.EntityFrameworkCore;
using TransformTemplateView.Data.Models;

namespace TransformTemplateView.Data;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions options)
        : base(options)
    {
    }
    public DbSet<Employee> Employees => Set<Employee>();
}
