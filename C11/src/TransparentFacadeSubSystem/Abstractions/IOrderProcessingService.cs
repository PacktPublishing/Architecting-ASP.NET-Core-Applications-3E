namespace TransparentFacadeSubSystem.Abstractions;

public interface IOrderProcessingService
{
    int CreateOrder(string productId, int quantity);
    string GetOrderStatus(int orderId);
}