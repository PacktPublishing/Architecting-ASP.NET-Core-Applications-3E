namespace TransparentFacadeSubSystem;

// Subsystem: Order Processing
public class OrderProcessingService
{
    public int CreateOrder(string productId, int quantity)
    {
        // Logic to create an order
        return 123; // Returns a mock order ID
    }

    public string GetOrderStatus(int orderId)
    {
        // Logic to get order status
        return "Order Shipped"; // Simplified for example
    }
}
