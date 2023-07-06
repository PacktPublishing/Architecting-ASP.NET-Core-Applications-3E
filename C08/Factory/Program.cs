using Factory.Models;
using Factory.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IHomeService, HomeService>()
    .AddTransient(serviceProvider =>
    {
        var homeService = serviceProvider.GetRequiredService<IHomeService>();
        var data = homeService.GetHomePageData();
        return new HomePageViewModel(data);
    })
    .AddSingleton<IHomeViewModelFactory, HomeViewModelFactory>()
    .AddControllersWithViews()
;
var app = builder.Build();
app.MapDefaultControllerRoute();
app.MapGet("/", (HttpContext context) => new[] {
    $"https://{context.Request.Host}/service-locator",
    $"https://{context.Request.Host}/method-injection",
    $"https://{context.Request.Host}/constructor-injection",
    $"https://{context.Request.Host}/minimal-api",
});
app.Run();
