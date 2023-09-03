using REPR.Products.Contracts;
using REPR.Products.Data;

namespace REPR.Products.Features;

[Mapper]
public partial class DeleteProductMapper
{
    public partial ProductDeleted MapToIntegrationEvent(Product product);
    public partial DeleteProductResponse MapToResponse(Product product);
}
