using Microsoft.EntityFrameworkCore;
using VerticalApp.Models;

namespace VerticalApp.Data;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}
