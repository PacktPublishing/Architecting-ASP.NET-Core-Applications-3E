namespace CoreConcepts;

public class Container
{
    public Client GetClientService()
    {
        // Inversion of Control
        var service = new Service(); // Dependency
        var client = new Client(service); // Dependency injection
        return client;
    }
}

public class Client(Service service) // Dependency
{
    private readonly Service _service = service;
    public void Operation()
    {
        // Using but not controlling the dependency
        _service.ExecuteSomeTask();
    }
}

public class Service
{
    public void ExecuteSomeTask()
        => throw new NotImplementedException();
}