using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using REPR.Products.Contracts;
using REPR.Products.Data;
using Riok.Mapperly.Abstractions;

namespace REPR.Products.Features;

public static partial class Delete
{
    public record class Command(int ProductId);
    public record class Response(int Id, string Name, decimal UnitPrice);

    [Mapper]
    public partial class Mapper
    {
        public partial ProductDeleted MapToIntegrationEvent(Product product);
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
            var product = await _db.Products.FirstOrDefaultAsync(
                x => x.Id == command.ProductId,
                cancellationToken: cancellationToken
            );
            if (product is null)
            {
                throw new ProductNotFoundException(command.ProductId);
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync(cancellationToken);

            var productDeleted = _mapper.MapToIntegrationEvent(product);
            await _bus.Publish(productDeleted, CancellationToken.None);

            var result = _mapper.MapToResponse(product);
            return result;
        }
    }

    public static IServiceCollection AddDelete(this IServiceCollection services)
    {
        return services
            .AddScoped<Handler>()
            .AddSingleton<Mapper>()
        ;
    }

    public static IEndpointRouteBuilder MapDelete(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(
            "/{ProductId}",
            ([AsParameters] Command query, Handler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(query, cancellationToken)
        );
        return endpoints;
    }
}
