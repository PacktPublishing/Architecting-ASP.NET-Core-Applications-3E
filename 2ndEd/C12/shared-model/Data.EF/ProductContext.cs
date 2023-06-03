using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.EF;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
