using System.Numerics;

namespace NinjaISP;

public class Ninja : IAttackable, IAttacker
{
    private readonly Weapon _meleeWeapon;
    private readonly Weapon _rangedWeapon;

    public string Name { get; }
    public Vector2 Position { get; set; }

    public Ninja(string name, Weapon meleeWeapon, Weapon rangedWeapon, Vector2? position = null)
    {
        Name = name;
        Position = position ?? Vector2.Zero;
        _meleeWeapon = meleeWeapon;
        _rangedWeapon = rangedWeapon;
    }

    public AttackResult Attack(IAttackable target)
    {
        var distance = this.DistanceFrom(target);
        if (_meleeWeapon.CanHit(distance))
        {
            return new AttackResult(_meleeWeapon, this, target);
        }
        else
        {
            return new AttackResult(_rangedWeapon, this, target);
        }
    }
}
