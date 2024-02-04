using ForEvolve.ExceptionMapper;

namespace Baskets;

public class BasketItemNotFoundException : NotFoundException
{
    public BasketItemNotFoundException(int productId)
        : base($"The product '{productId}' is not in your shopping cart.")
    {
    }
}
