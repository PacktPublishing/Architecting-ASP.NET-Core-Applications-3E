using Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Data.EF;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
