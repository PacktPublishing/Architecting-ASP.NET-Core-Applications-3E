using Abstractions;

namespace Core;
public class SomeService
{
    public void Operation(IDataPersistence someDataPersistence)
    {
        // The someDataPersistence instance is responsible
        // for the location where the data is persisted.
        Console.WriteLine("Beginning SomeService.Operation.");
        someDataPersistence.Persist();
        Console.WriteLine("SomeService.Operation has ended.");
    }
}
