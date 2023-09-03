namespace REPR.Baskets.Features;

[Mapper]
public partial class UpdateQuantityMapper
{
    public partial BasketItem Map(UpdateQuantityCommand item);
    public partial UpdateQuantityResponse Map(BasketItem item);
}
