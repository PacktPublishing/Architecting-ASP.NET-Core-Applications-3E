using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Singleton
{
    public class MyAmbientContextTest
    {
        public class Current : MyAmbientContextTest
        {
            [Fact]
            public void Should_always_return_the_same_instance()
            {
                var first = MyAmbientContext.Current;
                var second = MyAmbientContext.Current;
                Assert.Same(first, second);
            }
        }

        public class WriteSomething : MyAmbientContextTest
        {
            [Fact]
            public void Should_echo_the_inputted_text_to_the_console()
            {
                // Arrange (make the console write to a StringBuilder 
                // instead of the actual console)
                var expectedText = "This is your something: Hello World!" + Environment.NewLine;
                var sb = new StringBuilder();
                using (var writer = new StringWriter(sb))
                {
                    Console.SetOut(writer);

                    // Act
                    MyAmbientContext.Current.WriteSomething("Hello World!");
                }

                // Assert
                var actualText = sb.ToString();
                Assert.Equal(expectedText, actualText);
            }
        }
    }
}
