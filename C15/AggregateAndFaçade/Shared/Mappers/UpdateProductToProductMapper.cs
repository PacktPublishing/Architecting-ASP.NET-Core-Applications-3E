using Shared.Contracts;
using Shared.Models;

namespace Shared.Mappers;

public class UpdateProductToProductMapper : IMapper<UpdateProduct, Product>
{
    public Product Map(UpdateProduct entity)
    {
        return new(entity.Name, entity.quantityInStock, entity.Id);
    }
}