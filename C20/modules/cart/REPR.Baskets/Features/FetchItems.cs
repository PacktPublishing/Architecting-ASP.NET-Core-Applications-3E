using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using REPR.Baskets.Data;
using Riok.Mapperly.Abstractions;
using System.Collections;

namespace REPR.Baskets.Features;
public static partial class FetchItems
{
    public record class Query(int CustomerId);
    public record class Response(IEnumerable<Item> Items) : IEnumerable<Item>
    {
        public IEnumerator<Item> GetEnumerator()
            => Items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => ((IEnumerable)Items).GetEnumerator();
    }

    public record class Item(int ProductId, int Quantity);

    [Mapper]
    public partial class Mapper
    {
        public partial Response Map(IQueryable<BasketItem> items);
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);
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

        public async Task<Response> HandleAsync(Query query, CancellationToken cancellationToken)
        {
            var items = _db.Items.Where(x => x.CustomerId == query.CustomerId);
            await items.LoadAsync(cancellationToken);
            var result = _mapper.Map(items);
            return result;
        }
    }
    public static IServiceCollection AddFetchItems(this IServiceCollection services)
    {
        return services
            .AddScoped<Handler>()
            .AddSingleton<Mapper>()
        ;
    }

    public static IEndpointRouteBuilder MapFetchItems(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/{CustomerId}",
            ([AsParameters] Query query, Handler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(query, cancellationToken)
        );
        return endpoints;
    }
}

