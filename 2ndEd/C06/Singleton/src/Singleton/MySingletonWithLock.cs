namespace Singleton
{
    public class MySingletonWithLock
    {
        private readonly static object _myLock = new();

        private static MySingletonWithLock? _instance;
        private MySingletonWithLock() { }

        public static MySingletonWithLock Create()
        {
            lock (_myLock)
            {
                if (_instance == default)
                {
                    _instance = new MySingletonWithLock();
                }
            }
            return _instance;
        }
    }
}
