using ForEvolve.ExceptionMapper;

namespace REPR.Products;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(int productId)
        : base($"The product '{productId}' was not found.")
    {

    }
}
