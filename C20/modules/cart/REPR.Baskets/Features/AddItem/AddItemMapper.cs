namespace REPR.Baskets.Features;

[Mapper]
public partial class AddItemMapper
{
    public partial BasketItem Map(AddItemCommand item);
    public partial AddItemResponse Map(BasketItem item);
}
