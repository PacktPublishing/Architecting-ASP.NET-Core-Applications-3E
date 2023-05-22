namespace Composite.Models;

public class Book : IComponent
{
    public Book(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public string Title { get; set; }
    public string Type => "Book";

    public int Count() => 1;
    public string Display() => $"{Title} <small class=\"text-muted\">({Type})</small>";

    public void Add(IComponent bookComponent) => throw new NotSupportedException();
    public void Remove(IComponent bookComponent) => throw new NotSupportedException();
}
