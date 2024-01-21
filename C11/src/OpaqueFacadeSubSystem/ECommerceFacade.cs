using OpaqueFacadeSubSystem.Abstractions;

namespace OpaqueFacadeSubSystem;

public class ECommerceFacade : IECommerceOpaqueFacade
{
    private readonly InventoryService _inventoryService;
    private readonly OrderProcessingService _orderProcessingService;
    private readonly ShippingService _shippingService;

    public ECommerceFacade(InventoryService inventoryService, OrderProcessingService orderProcessingService, ShippingService shippingService)
    {
        _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
        _orderProcessingService = orderProcessingService ?? throw new ArgumentNullException(nameof(orderProcessingService));
        _shippingService = shippingService ?? throw new ArgumentNullException(nameof(shippingService));
    }

    public string PlaceOrder(string productId, int quantity)
    {
        if (_inventoryService.CheckStock(productId, quantity))
        {
            var orderId = _orderProcessingService.CreateOrder(productId, quantity);
            _shippingService.ScheduleShipping(orderId);
            return $"Order {orderId} placed successfully.";
        }
        return "Order failed due to insufficient stock.";
    }

    public string CheckOrderStatus(int orderId)
    {
        return _orderProcessingService.GetOrderStatus(orderId);
    }
}
