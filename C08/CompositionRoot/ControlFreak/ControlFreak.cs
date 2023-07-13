namespace CompositionRoot.ControlFreak;

public class Consumer
{
    public void Do()
    {
        var dependency = new Dependency();
        dependency.Operation();
    }
}

public class Dependency
{
    public void Operation()
        => throw new NotImplementedException();
}

public class DIEnabledConsumer
{
    private readonly Dependency _dependency;
    public DIEnabledConsumer(Dependency dependency)
    {
        _dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
    }

    public void Do()
    {
        _dependency.Operation();
    }
}