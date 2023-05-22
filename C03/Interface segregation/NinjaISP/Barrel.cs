using System.Numerics;

namespace NinjaISP;

public class Barrel : IAttackable
{
    public string Name => nameof(Barrel);
    public Vector2 Position { get; set; }
}
