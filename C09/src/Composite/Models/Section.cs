namespace Composite.Models;

public class Section : BookComposite
{
    public Section(string name) : base(name) { }

    protected override string HeadingTagName => "h3";
}
