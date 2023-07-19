namespace Composite.Models;

public class Corporation : BookComposite
{
    public Corporation(string name, string ceo)
        : base(name)
    {
        CEO = ceo;
    }

    public string CEO { get; }
}
