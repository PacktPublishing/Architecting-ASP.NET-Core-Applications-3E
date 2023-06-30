namespace Singleton;

public class MySingletonWithLock
{
    private static readonly object _myLock = new();

    private static MySingletonWithLock? _instance;
    private MySingletonWithLock() { }

    public static MySingletonWithLock Create()
    {
        lock (_myLock)
        {
            _instance ??= new MySingletonWithLock();
        }
        return _instance;
    }
}
