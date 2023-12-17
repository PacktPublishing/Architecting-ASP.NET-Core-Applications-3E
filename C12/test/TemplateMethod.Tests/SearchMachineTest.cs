namespace TemplateMethod;
public class SearchMachineTest
{
    public class Ctor : SearchMachineTest
    {
        [Fact]
        public void Should_guard_against_null_values()
        {
            Assert.Throws<ArgumentNullException>(() => new FakeSearchMachine(null!));
        }
    }

    public class IndexOf : SearchMachineTest
    {
        [Fact]
        public void Should_return_null_when_Values_is_empty()
        {
            // Arrange
            var searchMachine = new FakeSearchMachine();

            // Act
            var result = searchMachine.IndexOf(5);

            // Assert
            Assert.Null(result);
        }
    }

    private class FakeSearchMachine : SearchMachine
    {
        public FakeSearchMachine(params int[] values)
            : base(values) { }

        protected override int? Find(int value)
        {
            throw new NotImplementedException();
        }
    }
}
