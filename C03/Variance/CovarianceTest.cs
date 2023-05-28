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

public interface ICovariant<out T>
{
    T Get();
}

public class SwordGetter : ICovariant<Sword>
{
    private static readonly Sword _instance = new();
    public Sword Get() => _instance;
}
