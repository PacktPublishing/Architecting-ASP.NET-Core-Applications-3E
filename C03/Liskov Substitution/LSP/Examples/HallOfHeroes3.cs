using LSP.Models;

namespace LSP.Examples.Update3;

public class HallOfHeroes : HallOfFame
{
    public event EventHandler<AddingDuplicateNinjaEventArgs> AddingDuplicateNinja;

    public override void Add(Ninja ninja)
    {
        if (InternalMembers.Contains(ninja))
        {
            OnAddingDuplicateNinja(new AddingDuplicateNinjaEventArgs(ninja));
            return;
        }
        InternalMembers.Add(ninja);
    }

    protected virtual void OnAddingDuplicateNinja(AddingDuplicateNinjaEventArgs e)
    {
        AddingDuplicateNinja?.Invoke(this, e);
    }
}

public class AddingDuplicateNinjaEventArgs : EventArgs
{
    public Ninja DuplicatedNinja { get; }

    public AddingDuplicateNinjaEventArgs(Ninja ninja)
    {
        DuplicatedNinja = ninja ?? throw new ArgumentNullException(nameof(ninja));
    }
}
