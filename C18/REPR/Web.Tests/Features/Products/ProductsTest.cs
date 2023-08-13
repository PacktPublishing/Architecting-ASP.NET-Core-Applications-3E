using static Web.Features.Products;

namespace Web.Features;

public partial class ProductsTest
{
    private static async Task SeederDelegateAsync(ProductContext db)
    {
        db.Products.RemoveRange(db.Products.ToArray());
        await db.Products.AddAsync(new Product(
            Name: "Scotch Bottle",
            Id: 2
        ));
        await db.Products.AddAsync(new Product(
            Name: "Habanero Pepper",
            Id: 3
        ));
        await db.SaveChangesAsync();
    }
}
