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
    (PlaceOrder order, IOpaqueFacade opaqueFacade)
        => opaqueFacade.ExecuteOperationA()
);
app.MapGet(
    "/opaque/CheckOrderStatus",
    (int orderId, IOpaqueFacade opaqueFacade)
        => opaqueFacade.ExecuteOperationB()
);
app.MapPost(
    "/transparent/PlaceOrder",
    (PlaceOrder order, IECommerceTransparentFacade eCommerceFacade)
        => eCommerceFacade.PlaceOrder(order.ProductId, order.Quantity)
);
app.MapGet(
    "/transparent/CheckOrderStatus/{orderId}",
    (int orderId, IECommerceTransparentFacade eCommerceFacade)
        => eCommerceFacade.CheckOrderStatus(orderId)
);
app.Run();

public record class PlaceOrder(string ProductId, int Quantity);