namespace CoreConcepts;

public class ObjectLifetime : ITransient, IScoped, ISingleton
{
    public Guid Id { get; } = Guid.NewGuid();
}

public interface ISingleton
{
    Guid Id { get; }
}
public interface IScoped
{
    Guid Id { get; }
}
public interface ITransient
{
    Guid Id { get; }
}

public class ServiceConsumer(ISingleton singleton, IScoped scoped, ITransient transient)
{
    private readonly ISingleton _singleton = singleton;
    private readonly IScoped _scoped = scoped;
    private readonly ITransient _transient = transient;

    public Guid SingletonId => _singleton.Id;
    public Guid ScopedId => _scoped.Id;
    public Guid TransientId => _transient.Id;
}