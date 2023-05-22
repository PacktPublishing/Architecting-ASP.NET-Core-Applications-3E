using Xunit;

namespace Variance;

public class CovarianceTest
{
    [Fact]
    public void Instanciation()
    {
        Sword sword = new Sword(); 
        Weapon weapon1 = new Sword(); // Covariance
        Sword weapon2 = new TwoHandedSword(); // Covariance
        Weapon weapon3 = new TwoHandedSword(); // Covariance

        // A Sword cannot be a TwoHandedSword (breaks Covariance)
        //TwoHandedSword twoHandedSword = new Sword(); // Compilation error
    }

    [Fact]
    public void Covariance_tests()
    {
        Assert.IsType<Sword>(Covariance());
        Assert.Throws<InvalidCastException>(() => BreakCovariance());
    }

    // We can return a Sword into a Weapon
    private Weapon Covariance()
        => new Sword();

    // We cannot return a Sword into a TwoHandedSword
    private TwoHandedSword BreakCovariance()
        => (TwoHandedSword)new Sword();
}

public class ContravarianceTest
{
    [Fact]
    public void Contravariance_tests()
    {
        // We can pass a Sword as a Weapon
        Contravariance(new Sword());

        // We cannot pass a Weapon as a Sword
        // BreakContravariance(new Weapon()); // Compilation error
    }

    private void Contravariance(Weapon weapon) { }
    private void BreakContravariance(Sword weapon) { }
}

public class Weapon { }
public class Sword : Weapon { }
public class TwoHandedSword : Sword { }
