using Shared.Contracts;
using Shared.Models;

namespace Shared.Mappers;

public class ProductToProductSummaryMapper : IMapper<Product, ProductSummary>
{
    public ProductSummary Map(Product entity)
    {
        if (entity.Id == null)
        {
            throw new EntityValidationException("Id cannot be null.");
        }
        return new(entity.Id.Value, entity.Name);
    }
}
