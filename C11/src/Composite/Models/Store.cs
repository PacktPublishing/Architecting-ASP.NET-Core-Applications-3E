namespace Composite.Models;

public class Store : BookComposite
{
    public Store(string name) : base(name) { }

    protected override string HeadingTagName => "h2";
}
