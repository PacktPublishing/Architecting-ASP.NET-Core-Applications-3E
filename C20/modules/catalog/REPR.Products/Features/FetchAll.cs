using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using REPR.Products.Data;
using Riok.Mapperly.Abstractions;

namespace REPR.Products.Features;

public static partial class FetchAll
{
    public record class Query();
    public record class Response(IEnumerable<ResponseProduct> Products);
    public record class ResponseProduct(int Id, string Name, decimal UnitPrice);

    [Mapper]
    public partial class Mapper
    {
        public partial IEnumerable<ResponseProduct> Project(IQueryable<Product> products);
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
            await _db.Products.LoadAsync(cancellationToken);
            var products = _mapper.Project(_db.Products.OrderBy(x => x.Name));
            return new Response(products);
        }
    }

    public static IServiceCollection AddFetchAll(this IServiceCollection services)
    {
        return services
            .AddScoped<Handler>()
            .AddSingleton<Mapper>()
        ;
    }

    public static IEndpointRouteBuilder MapFetchAll(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/",
            (Handler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(new Query(), cancellationToken)
        );
        return endpoints;
    }
}
