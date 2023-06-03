using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PageController.Data;
using PageController.TagHelpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EmployeeDbContext>(
    options => options.UseInMemoryDatabase(nameof(EmployeeDbContext))
);
builder.Services.AddRazorPages();
builder.Services
    .Configure<RssFeedTagHelperComponentOptions>(builder.Configuration.GetSection("RssFeed"))
    .AddTransient(sp => sp.GetRequiredService<IOptionsMonitor<RssFeedTagHelperComponentOptions>>().CurrentValue)
    .AddTransient<ITagHelperComponent, RssFeedTagHelperComponent>()
    .AddTransient<ITagHelperComponent, MinifierTagHelperComponent>()
;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
