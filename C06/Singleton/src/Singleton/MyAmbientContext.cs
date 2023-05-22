using System;

namespace Singleton
{
    public class MyAmbientContext
    {
        public static MyAmbientContext Current { get; } = new MyAmbientContext();

        private MyAmbientContext() { }

        public void WriteSomething(string something)
        {
            Console.WriteLine($"This is your something: {something}");
        }
    }
}
