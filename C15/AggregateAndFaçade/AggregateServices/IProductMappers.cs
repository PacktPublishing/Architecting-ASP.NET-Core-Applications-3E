using Shared.Contracts;
using Shared.Mappers;
using Shared.Models;

namespace AggregateServices;
public interface IProductMappers
{
    IMapper<Product, ProductSummary> EntityToDto { get; }
    IMapper<InsertProduct, Product> InsertDtoToEntity { get; }
    IMapper<UpdateProduct, Product> UpdateDtoToEntity { get; }
}
