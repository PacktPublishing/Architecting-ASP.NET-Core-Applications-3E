namespace NinjaShared;

public static class Logic
{
    public static async Task ExecuteSequenceAsync<T>(T theBluePhantom, T theUnseenMirage, Func<string, Task> writeAsync)
        where T : IAttacker
    {
        // The Blue Phantom attacks The Unseen Mirage with a first attack
        var result = theBluePhantom.Attack(theUnseenMirage);
        await PrintAttackResultAsync(result);

        // The Unseen Mirage moves away from The Blue Phantom
        theUnseenMirage.MoveTo(5, 5);
        await PrintMovementAsync(theUnseenMirage);

        // The Blue Phantom attacks The Unseen Mirage with a second attack
        var result2 = theBluePhantom.Attack(theUnseenMirage);
        await PrintAttackResultAsync(result2);

        // The Unseen Mirage moves further away from The Blue Phantom
        theUnseenMirage.MoveTo(20, 20);
        await PrintMovementAsync(theUnseenMirage);

        // The Blue Phantom attacks The Unseen Mirage with a third attack
        var result3 = theBluePhantom.Attack(theUnseenMirage);
        await PrintAttackResultAsync(result3);

        // The Unseen Mirage strikes back at The Blue Phantom from a distance
        var result4 = theUnseenMirage.Attack(theBluePhantom);
        await PrintAttackResultAsync(result4);

        // Output
        async Task PrintAttackResultAsync(AttackResult attackResult)
        {
            if (attackResult.Succeeded)
            {
                await writeAsync($"{attackResult.Attacker} hits {attackResult.Target} using {attackResult.Weapon} at distance {attackResult.Distance}!{Environment.NewLine}");
            }
            else
            {
                await writeAsync($"{attackResult.Attacker} misses {attackResult.Target} using {attackResult.Weapon} at distance {attackResult.Distance}...{Environment.NewLine}");
            }
        }

        async Task PrintMovementAsync(IAttackable ninja)
        {
            await writeAsync($"{ninja.Name} moved to {ninja.Position}.{Environment.NewLine}");
        }
    }
}
