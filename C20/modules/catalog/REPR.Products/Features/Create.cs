using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using REPR.Products.Contracts;
using REPR.Products.Data;
using Riok.Mapperly.Abstractions;
using System;

namespace REPR.Products.Features;

public static partial class Create
{
    public record class Command(string Name, decimal UnitPrice);
    public record class Response(int Id, string Name, decimal UnitPrice);

    [Mapper]
    public partial class Mapper
    {
        public partial Product Map(Command product);
        public partial ProductCreated MapToIntegrationEvent(Product product);
        public partial Response MapToResponse(Product product);
    }

    public class Handler
    {
        private readonly ProductContext _db;
        private readonly Mapper _mapper;
        private readonly IBus _bus;

        public Handler(ProductContext db, Mapper mapper, IBus bus)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            var product = _mapper.Map(command);
            var entry = _db.Products.Add(product);
            await _db.SaveChangesAsync(cancellationToken);

            var productAdded = _mapper.MapToIntegrationEvent(entry.Entity);
            await _bus.Publish(productAdded, CancellationToken.None);

            var response = _mapper.MapToResponse(entry.Entity);
            return response;
        }
    }
    public static IServiceCollection AddCreate(this IServiceCollection services)
    {
        return services
            .AddScoped<Handler>()
            .AddSingleton<Mapper>()
        ;
    }

    public static IEndpointRouteBuilder MapCreate(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/",
            async (Command query, Handler handler, CancellationToken cancellationToken) => {
                var result = await handler.HandleAsync(query, cancellationToken);
                return TypedResults.Created($"/products/{result.Id}", result);
            }
        );
        return endpoints;
    }
}
