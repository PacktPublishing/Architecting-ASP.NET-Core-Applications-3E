namespace Products.Features;

[Mapper]
public partial class DeleteProductMapper
{
    public partial ProductDeleted MapToIntegrationEvent(Product product);
    public partial DeleteProductResponse MapToResponse(Product product);
}
