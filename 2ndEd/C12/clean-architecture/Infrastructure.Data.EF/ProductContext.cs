using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.EF;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
