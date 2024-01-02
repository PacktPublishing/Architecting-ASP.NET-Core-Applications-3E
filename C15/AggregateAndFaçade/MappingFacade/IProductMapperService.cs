using Shared.Contracts;
using Shared.Mappers;
using Shared.Models;

namespace MappingFacade;
public interface IProductMapperService :
    IMapper<Product, ProductSummary>,
    IMapper<InsertProduct, Product>,
    IMapper<UpdateProduct, Product>
{
}
