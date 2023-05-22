namespace Composite.Models;

public class Corporation : BookComposite
{
    public Corporation(string name) : base(name) { }

    protected override string HeadingTagName => "h1";
}
