namespace CoreConcepts.Raw;
public class Client
{
    public void Operation()
    {
        // Direct control over dependency
        var service = new Service(); // Dependency
        service.ExecuteSomeTask();
    }
}

public class Service
{
    public void ExecuteSomeTask()
        => throw new NotImplementedException();
}