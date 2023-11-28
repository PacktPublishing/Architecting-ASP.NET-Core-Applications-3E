namespace CoreConcepts;
public class RawClient
{
    public void Operation()
    {
        // Direct control over dependency
        var service = new RawService(); // Dependency
        service.ExecuteSomeTask();
    }
}

public class RawService
{
    public void ExecuteSomeTask()
        => throw new NotImplementedException();
}