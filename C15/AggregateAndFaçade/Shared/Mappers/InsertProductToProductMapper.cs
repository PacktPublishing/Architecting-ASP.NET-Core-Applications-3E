using Shared.Contracts;
using Shared.Models;

namespace Shared.Mappers;

public class InsertProductToProductMapper : IMapper<InsertProduct, Product>
{
    public Product Map(InsertProduct entity)
    {
        return new(entity.Name, quantityInStock: 0);
    }
}
