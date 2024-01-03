using Shared.Contracts;
using Shared.Mappers;
using Shared.Models;

namespace MappingFacade;

public class ProductMapperService : IProductMapperService
{
    private readonly IMapper<Product, ProductSummary> _entityToDto;
    private readonly IMapper<InsertProduct, Product> _insertDtoToEntity;
    private readonly IMapper<UpdateProduct, Product> _updateDtoToEntity;

    public ProductMapperService(IMapper<Product, ProductSummary> entityToDto, IMapper<InsertProduct, Product> insertDtoToEntity, IMapper<UpdateProduct, Product> updateDtoToEntity)
    {
        _entityToDto = entityToDto ?? throw new ArgumentNullException(nameof(entityToDto));
        _insertDtoToEntity = insertDtoToEntity ?? throw new ArgumentNullException(nameof(insertDtoToEntity));
        _updateDtoToEntity = updateDtoToEntity ?? throw new ArgumentNullException(nameof(updateDtoToEntity));
    }

    public ProductSummary Map(Product entity)
    {
        return _entityToDto.Map(entity);
    }
    public Product Map(InsertProduct dto)
    {
        return _insertDtoToEntity.Map(dto);
    }
    public Product Map(UpdateProduct dto)
    {
        return _updateDtoToEntity.Map(dto);
    }
}
