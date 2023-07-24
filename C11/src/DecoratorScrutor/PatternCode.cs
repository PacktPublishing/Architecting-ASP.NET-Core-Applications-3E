namespace DecoratorScrutor;

public interface IComponent
{
    string Operation();
}

public class ComponentA : IComponent
{
    public string Operation()
    {
        return "Hello from ComponentA";
    }
}

public class DecoratorA : IComponent
{
    private readonly IComponent _component;

    public DecoratorA(IComponent component)
    {
        _component = component ?? throw new ArgumentNullException(nameof(component));
    }

    public string Operation()
    {
        var result = _component.Operation();
        return $"<DecoratorA>{result}</DecoratorA>";
    }
}

public class DecoratorB : IComponent
{
    private readonly IComponent _component;

    public DecoratorB(IComponent component)
    {
        _component = component ?? throw new ArgumentNullException(nameof(component));
    }

    public string Operation()
    {
        var result = _component.Operation();
        return $"<DecoratorB>{result}</DecoratorB>";
    }
}
