using REPR.Products.Data;

namespace REPR.Products.Features;

[Mapper]
public partial class FetchOneProductMapper
{
    public partial FetchOneProductResponse Map(Product product);
}
