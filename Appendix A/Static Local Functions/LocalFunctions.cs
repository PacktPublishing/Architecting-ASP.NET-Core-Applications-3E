using Xunit;

namespace LocalFunctions;

public class Tests
{
    public class LocalFunction
    {
        [Fact]
        public void With_no_parameter_accessing_outer_scope()
        {
            var x = 1;
            var y = 2;
            var z = Add();
            Assert.Equal(3, z);

            x = 2;
            y = 3;
            var n = Add();
            Assert.Equal(5, n);

            int Add()
            {
                return x + y;
            }
        }

        [Fact]
        public void With_one_parameter_accessing_outer_scope()
        {
            var x = 1;
            var z = Add(2);
            Assert.Equal(3, z);

            x = 2;
            var n = Add(3);
            Assert.Equal(5, n);

            int Add(int y)
            {
                return x + y;
            }
        }

        [Fact]
        public void With_two_parameters_not_accessing_outer_scope()
        {
            var a = Add(1, 2);
            Assert.Equal(3, a);

            var b = Add(2, 3);
            Assert.Equal(5, b);

            int Add(int x, int y)
            {
                return x + y;
            }
        }

        [Fact]
        public void With_two_parameters_accessing_outer_scope()
        {
            var z = 5;
            var a = Add(1, 2);
            Assert.Equal(8, a);

            var b = Add(2, 3);
            Assert.Equal(10, b);

            int Add(int x, int y)
            {
                return x + y + z;
            }
        }
    }

    public class StaticLocalFunction
    {
        [Fact]
        public void With_two_parameters()
        {
            var a = Add(1, 2);
            Assert.Equal(3, a);

            var b = Add(2, 3);
            Assert.Equal(5, b);

            static int Add(int x, int y)
            {
                return x + y;
            }
        }

        [Fact]
        public void With_three_parameters()
        {
            var c = 5;
            var a = Add(1, 2, c);
            Assert.Equal(8, a);

            var b = Add(2, 3, c);
            Assert.Equal(10, b);

            static int Add(int x, int y, int z)
            {
                return x + y + z;
            }
        }
    }
}
