using OpaqueFacadeSubSystem.Abstractions;
using TransparentFacadeSubSystem.Abstractions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddOpaqueFacadeSubSystem()
    .AddTransparentFacadeSubSystem()
    //.AddSingleton<IComponentB, UpdatedComponentB>()
;

var app = builder.Build();
app.MapPost(
    "/opaque/PlaceOrder",
    (PlaceOrder order, IECommerceOpaqueFacade eCommerceOpaqueFacade)
        => eCommerceOpaqueFacade.PlaceOrder(order.ProductId, order.Quantity)
);
app.MapGet(
    "/opaque/CheckOrderStatus/{orderId}",
    (int orderId, IECommerceOpaqueFacade eCommerceOpaqueFacade)
        => eCommerceOpaqueFacade.CheckOrderStatus(orderId)
);
app.MapPost(
    "/transparent/PlaceOrder",
    (PlaceOrder order, IECommerceTransparentFacade eCommerceTransparentFacade)
        => eCommerceTransparentFacade.PlaceOrder(order.ProductId, order.Quantity)
);
app.MapGet(
    "/transparent/CheckOrderStatus/{orderId}",
    (int orderId, IECommerceTransparentFacade eCommerceTransparentFacade)
        => eCommerceTransparentFacade.CheckOrderStatus(orderId)
);
app.Run();

public record class PlaceOrder(string ProductId, int Quantity);