using Xunit;

namespace LSP.Examples.Update2;

public class HallOfHeroesTest : BaseLSPTest
{
    protected override HallOfFame sut { get; } = new HallOfHeroes();

    [Trait("FailureExpected", "true")]
    public override void Add_should_not_add_existing_ninja()
    {
        base.Add_should_not_add_existing_ninja();
    }
}
