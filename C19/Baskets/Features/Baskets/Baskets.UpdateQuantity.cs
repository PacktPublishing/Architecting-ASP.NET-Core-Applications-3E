using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Riok.Mapperly.Abstractions;
namespace Web.Features;

public partial class Baskets
{
    public partial class UpdateQuantity
    {
        public record class Command(int CustomerId, int ProductId, int Quantity);
        public record class Response(int ProductId, int Quantity);

        [Mapper]
        public partial class Mapper
        {
            public partial BasketItem Map(Command item);
            public partial Response Map(BasketItem item);
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.CustomerId).GreaterThan(0);
                RuleFor(x => x.ProductId).GreaterThan(0);
                RuleFor(x => x.Quantity).GreaterThan(0);
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
                var item = await _db.Items.AsNoTracking().FirstOrDefaultAsync(
                    x => x.CustomerId == command.CustomerId && x.ProductId == command.ProductId,
                    cancellationToken: cancellationToken
                );
                if (item is null)
                {
                    throw new BasketItemNotFoundException(command.ProductId);
                }
                var itemToUpdate = item with { Quantity = command.Quantity };
                if (item.Quantity != command.Quantity)
                {
                    _db.Items.Update(itemToUpdate);
                    await _db.SaveChangesAsync(cancellationToken);
                }
                var result = _mapper.Map(itemToUpdate);
                return result;
            }
        }
    }

    public static IServiceCollection AddUpdateQuantity(this IServiceCollection services)
    {
        return services
            .AddScoped<UpdateQuantity.Handler>()
            .AddSingleton<UpdateQuantity.Mapper>()
        ;
    }

    public static IEndpointRouteBuilder MapUpdateQuantity(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut(
            "/",
            (UpdateQuantity.Command command, UpdateQuantity.Handler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(command, cancellationToken)
        );
        return endpoints;
    }
}
