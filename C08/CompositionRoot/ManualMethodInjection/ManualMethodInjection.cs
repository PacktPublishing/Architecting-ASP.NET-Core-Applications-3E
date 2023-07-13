namespace CompositionRoot.ManualMethodInjection;

public class Subject
{
    public int Operation(Context context)
    {
        // ...
        return context.Number;
    }
}

public class Context
{
    public required int Number { get; init; }
}
