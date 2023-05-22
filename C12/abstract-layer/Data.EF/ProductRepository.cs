using Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext _db;
    public ProductRepository(ProductContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<IEnumerable<Product>> AllAsync(CancellationToken cancellationToken)
    {
        return await _db.Products.ToArrayAsync(cancellationToken);
    }

    public async Task CreateAsync(Product product, CancellationToken cancellationToken)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int productId, CancellationToken cancellationToken)
    {
        var product = await FindByIdAsync(productId, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(productId);
        }
        _db.Products.Remove(product);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product?> FindByIdAsync(int productId, CancellationToken cancellationToken)
    {
        var product = await _db.Products.FindAsync(new object[] { productId }, cancellationToken);
        return product;
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _db.Entry(product).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken);
    }
}
