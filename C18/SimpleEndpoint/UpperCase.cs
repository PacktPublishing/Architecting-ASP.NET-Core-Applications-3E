namespace SimpleEndpoint;

public static class UpperCase
{
    public record class Request(string Text);
    public record class Response(string Text);
    public class Handler
    {
        public Response Handle(Request request)
        {
            return new Response(request.Text.ToUpper());
        }
    }

    public static IServiceCollection AddUpperCase(this IServiceCollection services)
    {
        return services.AddSingleton<Handler>();
    }

    public static IEndpointRouteBuilder MapUpperCase(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/upper-case/{Text}",
            ([AsParameters] Request query, Handler handler)
                => handler.Handle(query)
        );
        return endpoints;
    }
}
