using static Web.Features.Products;

namespace Web.Features;

public partial class ProductsTest
{
    private static async Task SeederDelegateAsync(ProductContext db)
    {
        db.Products.RemoveRange(db.Products.ToArray());
        await db.Products.AddAsync(new Product(
            Name: "Scotch Bottle",
            UnitPrice: 99,
            Id: 2
        ));
        await db.Products.AddAsync(new Product(
            Name: "Habanero Pepper",
            UnitPrice: 1,
            Id: 3
        ));
        await db.SaveChangesAsync();
    }
}
