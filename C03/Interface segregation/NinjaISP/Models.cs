using System.Numerics;

namespace NinjaISP;

public interface IAttacker : IAttackable
{
    AttackResult Attack(IAttackable target);
}
public interface IAttackable
{
    string Name { get; }
    Vector2 Position { get; set; }
}

public abstract class Weapon
{
    public abstract float MinRanged { get; }
    public abstract float MaxRanged { get; }

    public virtual string Name => GetType().Name;
    public virtual bool CanHit(float distance)
        => distance >= MinRanged && distance <= MaxRanged;
}

public class Sword : Weapon
{
    public override float MinRanged { get; } = 0;
    public override float MaxRanged { get; } = Vector2.Distance(Vector2.Zero, Vector2.One);
}

public class Shuriken : Weapon
{
    public override float MinRanged { get; } = Vector2.Distance(Vector2.Zero, Vector2.One);
    public override float MaxRanged { get; } = 20;
}

public class Pistol : Weapon
{
    public override float MinRanged { get; } = Vector2.Distance(Vector2.Zero, Vector2.One);
    public override float MaxRanged { get; } = 50;
}

public class AttackResult
{
    public string Weapon { get; }
    public string Attacker { get; }
    public string Target { get; }
    public bool Succeeded { get; }
    public float Distance { get; }

    public AttackResult(Weapon weapon, IAttacker attacker, IAttackable target)
    {
        Weapon = $"{weapon.Name} (Min: {weapon.MinRanged}, Max: {weapon.MaxRanged})";
        Attacker = $"{attacker.Name} (Position: {attacker.Position})";
        Target = $"{target.Name} (Position: {target.Position})";
        Distance = attacker.DistanceFrom(target);
        Succeeded = weapon.CanHit(Distance);
    }
}

public static class AttackableExtensions
{
    public static float DistanceFrom(this IAttackable attacker, IAttackable target)
    {
        return Vector2.Distance(attacker.Position, target.Position);
    }

    public static IAttackable MoveTo(this IAttackable target, float x, float y)
    {
        target.Position = new Vector2(x, y);
        return target;
    }
}
