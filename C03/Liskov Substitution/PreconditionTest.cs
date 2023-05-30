namespace LiskovSubstitution;

[Trait("FailureExpected", "true")]
public class PreconditionTest
{
    public static TheoryData<SuperClass> InstancesThatThrowsSuperExceptions = new TheoryData<SuperClass>()
    {
        // value < 0
        new SuperClass(),

        // value < -10 (ok)
        new SubClassOk(),

        // value < 10 (breaks LSP)
        new SubClassBreak(),
    };

    /// <summary>
    /// Any precondition implemented in a supertype should yield the same outcome in its subtypes,
    /// but subtypes can be less strict about it, never more.
    /// </summary>
    /// <example>
    /// If a supertype validates that an argument cannot be null,
    /// the subtype could remove that validation but not add stricter validation rules.
    /// </example>
    [Theory]
    [MemberData(nameof(InstancesThatThrowsSuperExceptions))]
    public void Precondition_implemented_in_a_supertype_should_yield_the_same_outcome_in_its_subtypes(SuperClass sut)
    {
        var value = 5;
        var result = sut.IsValid(value);
        Console.WriteLine($"Do something with {result}");
    }

    public class SuperClass
    {
        public virtual bool IsValid(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value must be positive.", nameof(value));
            }
            return true;
        }
    }
    public class SubClassOk : SuperClass
    {
        public override bool IsValid(int value)
        {
            if (value < -10)
            {
                throw new ArgumentException("Value must be greater or equal to -10.", nameof(value));
            }
            return true;
        }
    }
    public class SubClassBreak : SuperClass
    {
        public override bool IsValid(int value)
        {
            if (value < 10) // Break LSP
            {
                throw new ArgumentException("Value must be greater than 10.", nameof(value));
            }
            return true;
        }
    }
}
