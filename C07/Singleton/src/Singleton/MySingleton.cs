namespace Singleton;

public class MySingleton
{
    private static MySingleton? _instance;

    private MySingleton() { }

    public static MySingleton Create()
    {
        _instance ??= new MySingleton();
        return _instance;
    }
}
