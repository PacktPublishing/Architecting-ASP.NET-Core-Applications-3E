namespace LiskovSubstitution;

[Trait("FailureExpected", "true")]
public class PostconditionTest
{
    public static TheoryData<SuperClass> InstancesThatThrowsSuperExceptions = new TheoryData<SuperClass>()
    {
        // Not null Model
        new SuperClass(),

        // Not null SubModel (ok)
        new SubClassOk(),

        // null (breaks LSP)
        new SubClassBreak(),
    };

    /// <summary>
    /// Any postcondition implemented in a supertype should yield the same outcome in its subtypes,
    /// but subtypes can be more strict about it, never less.
    /// </summary>
    /// <example>
    /// If the supertype never returns null, the subtype should not return null either or risk
    /// breaking the consumers of the object that are not testing for null. 
    /// If the supertype does not guarantee the returned value cannot be null, then a subtype
    /// could decide never to return null, making both instances interchangeable.
    /// </example>
    [Theory]
    [MemberData(nameof(InstancesThatThrowsSuperExceptions))]
    public void Postcondition_implemented_in_a_supertype_should_yield_the_same_outcome_in_its_subtypes(SuperClass sut)
    {
        var value = 5;
        var result = sut.Do(value);
        Console.WriteLine($"Do something with {result.Value}");
    }

    public class SuperClass
    {
        public virtual Model Do(int value)
        {
            return new(value);
        }
    }
    public class SubClassOk : SuperClass
    {
        private int _doCount = 0; // History: add state = ok
        public override Model Do(int value)
        {
            var baseModel = base.Do(value);
            return new SubModel(baseModel.Value, ++_doCount);
        }
    }
    public class SubClassBreak : SuperClass
    {
        public override Model Do(int value)
        {
            if (value == 5)
            {
                return null;
            }
            return base.Do(value);
        }
    }
    public record class Model(int Value);
    public record class SubModel(int Value, int DoCount) : Model(Value);
}
