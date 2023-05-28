using Xunit;

namespace Variance;

public class CovarianceTest
{
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
}

public class ContravarianceTest
{
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