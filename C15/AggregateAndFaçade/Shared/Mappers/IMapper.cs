namespace Shared.Mappers;
public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource entity);
}
