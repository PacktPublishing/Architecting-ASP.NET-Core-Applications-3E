namespace LiskovSubstitution;

[Trait("FailureExpected", "true")]
public class ExceptionTest
{
    public static TheoryData<SuperClass> InstancesThatThrowsSuperExceptions = new TheoryData<SuperClass>()
    {
        // Throw SuperException (handled)
        new SuperClass(),

        // Throw SubException (handled)
        new SubClassOk(),

        // Throw AnotherException (breaks LSP)
        new SubClassBreak(),
    };

    /// <summary>
    /// A subtypes can't throw a new type of exception.
    /// </summary>
    [Theory]
    [MemberData(nameof(InstancesThatThrowsSuperExceptions))]
    public void A_subtypes_can_t_throw_a_new_type_of_exception(SuperClass sut)
    {
        try
        {
            sut.Do();
        }
        catch (SuperException ex)
        {
            // The program handles the SuperException,
            // so it is fine to throw a SubException.
        }
    }

    public class SuperClass
    {
        public virtual void Do()
            => throw new SuperException();
    }
    public class SubClassOk : SuperClass
    {
        public override void Do()
            => throw new SubException();
    }
    public class SubClassBreak : SuperClass
    {
        public override void Do()
            => throw new AnotherException();
    }
    public class SuperException : Exception { }
    public class SubException : SuperException { }
    public class AnotherException : Exception { }
}
