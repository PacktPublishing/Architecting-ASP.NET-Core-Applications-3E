using Microsoft.Extensions.DependencyInjection;

namespace Core.Mappers;
public class ServiceLocatorMappingService : IMappingService
{
    private readonly IServiceProvider _serviceProvider;
    public ServiceLocatorMappingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public TDestination Map<TSource, TDestination>(TSource entity)
    {
        var mapper = _serviceProvider.GetService<IMapper<TSource, TDestination>>();
        if (mapper == null)
        {
            throw new MapperNotFoundException(typeof(TSource), typeof(TDestination));
        }
        return mapper.Map(entity);
    }
}
