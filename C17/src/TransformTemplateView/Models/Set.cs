namespace TransformTemplateView.Models;

public class Set : BookComposite
{
    public Set(string name, params IComponent[] books)
        : base(name)
    {
        AddRange(books);
    }
}
