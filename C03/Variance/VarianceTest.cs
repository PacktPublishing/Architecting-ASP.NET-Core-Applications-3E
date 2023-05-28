using System;
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

    [Fact]
    public void Generic_Covariance_tests()
    {
        ICovariant<Sword> swordGetter = new SwordGetter();
        ICovariant<Weapon> weaponGetter = swordGetter; // Covariance
        Assert.Same(swordGetter, weaponGetter);

        Sword sword = swordGetter.Get();
        Weapon weapon = weaponGetter.Get();

        var isSwordASword = Assert.IsType<Sword>(sword);
        var isWeaponASword = Assert.IsType<Sword>(weapon);

        Assert.NotNull(isSwordASword);
        Assert.NotNull(isWeaponASword);
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

    [Fact]
    public void Generic_Contravariance_tests()
    {
        IContravariant<Weapon> weaponSetter = new WeaponSetter();
        IContravariant<Sword> swordSetter = weaponSetter; // Contravariance
        Assert.Same(swordSetter, weaponSetter);

        // Contravariance: Weapon > Sword > TwoHandedSword
        weaponSetter.Set(new Weapon());
        weaponSetter.Set(new Sword());
        weaponSetter.Set(new TwoHandedSword());

        // Compilation error: cannot convert from 'Variance.Weapon' to 'Variance.Sword'
        // Reason: for the compiler, swordSetter is a IContravariant<Sword>, not a IContravariant<Weapon>.
        //swordSetter.Set(new Weapon());

        // Contravariance: Sword > TwoHandedSword
        swordSetter.Set(new Sword());
        swordSetter.Set(new TwoHandedSword());
    }


    private void Contravariance(Weapon weapon) { }
    private void BreakContravariance(Sword weapon) { }
}

public class Weapon { }
public class Sword : Weapon { }
public class TwoHandedSword : Sword { }

public interface ICovariant<out T>
{
    T Get();
}
public interface IContravariant<in T>
{
    void Set(T a);
}

public class SwordGetter : ICovariant<Sword>
{
    private static readonly Sword _instance = new();
    public Sword Get() => _instance;
}

public class WeaponSetter : IContravariant<Weapon>
{
    private Weapon? _weapon;
    public void Set(Weapon value)
        => _weapon = value;
}