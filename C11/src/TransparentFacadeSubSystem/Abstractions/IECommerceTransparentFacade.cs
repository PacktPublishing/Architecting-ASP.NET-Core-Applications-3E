namespace TransparentFacadeSubSystem.Abstractions;

public interface IECommerceTransparentFacade
{
    string PlaceOrder(string productId, int quantity);
    string CheckOrderStatus(int orderId);
}
