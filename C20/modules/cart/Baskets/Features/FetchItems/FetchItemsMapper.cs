namespace Baskets.Features;

[Mapper]
public partial class FetchItemsMapper
{
    public partial FetchItemsResponse Map(IQueryable<BasketItem> items);
}

