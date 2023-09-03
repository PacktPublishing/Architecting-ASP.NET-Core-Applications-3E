using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using REPR.Baskets.Data;
using Riok.Mapperly.Abstractions;
namespace REPR.Baskets.Features;

public static partial class RemoveItem
{
    public record class Command(int CustomerId, int ProductId);
    public record class Response(int ProductId, int Quantity);

    [Mapper]
    public partial class Mapper
    {
        public partial Response Map(BasketItem item);
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleFor(x => x.ProductId).GreaterThan(0);
        }
    }

    public class Handler
    {
        private readonly BasketContext _db;
        private readonly Mapper _mapper;

        public Handler(BasketContext db, Mapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            var item = await _db.Items.FirstOrDefaultAsync(
                x => x.CustomerId == command.CustomerId && x.ProductId == command.ProductId,
                cancellationToken: cancellationToken
            );
            if (item is null)
            {
                throw new BasketItemNotFoundException(command.ProductId);
            }
            _db.Items.Remove(item);
            await _db.SaveChangesAsync(cancellationToken);
            var result = _mapper.Map(item);
            return result;
        }
    }

    public static IServiceCollection AddRemoveItem(this IServiceCollection services)
    {
        return services
            .AddScoped<Handler>()
            .AddSingleton<Mapper>()
        ;
    }

    public static IEndpointRouteBuilder MapRemoveItem(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(
            "/{customerId}/{productId}",
            ([AsParameters] Command command, Handler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(command, cancellationToken)
        );
        return endpoints;
    }
}
