namespace TransformTemplateView.Models;

public class Book : IComponent
{
    public Book(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public string Title { get; set; }

    public int Count() => 1;

    public void Add(IComponent bookComponent) => throw new NotSupportedException();
    public void Remove(IComponent bookComponent) => throw new NotSupportedException();
}
