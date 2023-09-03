using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using REPR.Products.Data;
using Riok.Mapperly.Abstractions;

namespace REPR.Products.Features;
public static partial class FetchOne
{
    public record class Query(int ProductId);
    public record class Response(int Id, string Name, decimal UnitPrice);

    [Mapper]
    public partial class Mapper
    {
        public partial Response Map(Product product);
    }

    public class Handler
    {
        private readonly ProductContext _db;
        private readonly Mapper _mapper;

        public Handler(ProductContext db, Mapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> HandleAsync(Query query, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FirstOrDefaultAsync(
                x => x.Id == query.ProductId,
                cancellationToken: cancellationToken
            );
            if (product is null)
            {
                throw new ProductNotFoundException(query.ProductId);
            }
            return _mapper.Map(product);
        }
    }
    public static IServiceCollection AddFetchOne(this IServiceCollection services)
    {
        return services
            .AddScoped<Handler>()
            .AddSingleton<Mapper>()
        ;
    }

    public static IEndpointRouteBuilder MapFetchOne(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/{ProductId}",
            ([AsParameters] Query query, Handler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(query, cancellationToken)
        );
        return endpoints;
    }
}
