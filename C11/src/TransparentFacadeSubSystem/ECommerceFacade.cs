using TransparentFacadeSubSystem.Abstractions;

namespace TransparentFacadeSubSystem;

public class ECommerceFacade : IECommerceTransparentFacade
{
    private readonly IInventoryService _inventoryService;
    private readonly IOrderProcessingService _orderProcessingService;
    private readonly IShippingService _shippingService;

    public ECommerceFacade(IInventoryService inventoryService, IOrderProcessingService orderProcessingService, IShippingService shippingService)
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
