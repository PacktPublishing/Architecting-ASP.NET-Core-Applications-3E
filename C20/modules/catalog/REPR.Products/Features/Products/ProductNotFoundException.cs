using ForEvolve.ExceptionMapper;

namespace Web.Features;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(int productId)
        : base($"The product '{productId}' was not found.")
    {

    }
}
