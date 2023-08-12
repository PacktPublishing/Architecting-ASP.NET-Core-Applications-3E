using Microsoft.EntityFrameworkCore;
using Riok.Mapperly.Abstractions;

namespace Web.Features;

public partial class Products
{
    public partial class FetchOne
    {
        public record class Query(int ProductId);
        public record class Response(int Id, string Name);

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
    }

    public static IServiceCollection AddFetchOne(this IServiceCollection services)
    {
        return services
            .AddScoped<FetchOne.Handler>()
            .AddSingleton<FetchOne.Mapper>()
        ;
    }

    public static IEndpointRouteBuilder MapFetchOne(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/{ProductId}",
            ([AsParameters] FetchOne.Query query, FetchOne.Handler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(query, cancellationToken)
        );
        return endpoints;
    }
}
