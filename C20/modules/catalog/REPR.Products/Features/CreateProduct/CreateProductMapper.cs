using REPR.Products.Contracts;
using REPR.Products.Data;

namespace REPR.Products.Features;

[Mapper]
public partial class CreateProductMapper
{
    public partial Product Map(CreateProductCommand product);
    public partial ProductCreated MapToIntegrationEvent(Product product);
    public partial CreateProductResponse MapToResponse(Product product);
}
