using Facade;
using OpaqueFacadeSubSystem.Abstractions;
using TransparentFacadeSubSystem.Abstractions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddOpaqueFacadeSubSystem()
    .AddTransparentFacadeSubSystem()
    //.AddSingleton<IComponentB, UpdatedComponentB>()
;

var app = builder.Build();
app.MapGet("/", (HttpContext context) =>
new[] {
    $"https://{context.Request.Host}/opaque/a",
    $"https://{context.Request.Host}/opaque/b",
    $"https://{context.Request.Host}/transparent/a",
    $"https://{context.Request.Host}/transparent/b"
});
app.MapGet("/opaque/a", (IOpaqueFacade opaqueFacade)
    => opaqueFacade.ExecuteOperationA());
app.MapGet("/opaque/b", (IOpaqueFacade opaqueFacade)
    => opaqueFacade.ExecuteOperationB());
app.MapGet("/transparent/a", (ITransparentFacade transparentFacade)
    => transparentFacade.ExecuteOperationA());
app.MapGet("/transparent/b", (ITransparentFacade transparentFacade)
    => transparentFacade.ExecuteOperationB());
app.Run();
