using Shared.Contracts;
using Shared.Mappers;
using Shared.Models;

namespace AggregateServices;

public class ProductMappers : IProductMappers
{
    public ProductMappers(IMapper<Product, ProductSummary> entityToDto, IMapper<InsertProduct, Product> insertDtoToEntity, IMapper<UpdateProduct, Product> updateDtoToEntity)
    {
        EntityToDto = entityToDto ?? throw new ArgumentNullException(nameof(entityToDto));
        InsertDtoToEntity = insertDtoToEntity ?? throw new ArgumentNullException(nameof(insertDtoToEntity));
        UpdateDtoToEntity = updateDtoToEntity ?? throw new ArgumentNullException(nameof(updateDtoToEntity));
    }

    public IMapper<Product, ProductSummary> EntityToDto { get; }
    public IMapper<InsertProduct, Product> InsertDtoToEntity { get; }
    public IMapper<UpdateProduct, Product> UpdateDtoToEntity { get; }
}
