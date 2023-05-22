namespace Singleton
{
    public class MySimpleSingleton
    {
        public static MySimpleSingleton Instance { get; } = new MySimpleSingleton();

        private MySimpleSingleton() { }
    }
}
