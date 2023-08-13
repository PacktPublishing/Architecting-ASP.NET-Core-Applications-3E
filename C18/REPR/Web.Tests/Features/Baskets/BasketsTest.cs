
using static Web.Features.Baskets;

namespace Web.Features;
public partial class BasketsTest
{
    private static async Task SeederDelegateAsync(BasketContext db)
    {
        db.Items.RemoveRange(db.Items.ToArray());
        await db.Items.AddAsync(new BasketItem(1, 1, 10));
        await db.Items.AddAsync(new BasketItem(1, 2, 20));
        await db.Items.AddAsync(new BasketItem(1, 3, 30));
        await db.Items.AddAsync(new BasketItem(2, 1, 5));
        await db.Items.AddAsync(new BasketItem(2, 3, 15));
        await db.Items.AddAsync(new BasketItem(3, 2, 18));
        await db.Items.AddAsync(new BasketItem(3, 4, 36));
        await db.SaveChangesAsync();
    }
}
