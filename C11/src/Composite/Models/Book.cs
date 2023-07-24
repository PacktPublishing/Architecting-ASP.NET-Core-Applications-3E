namespace Composite.Models;

public class Book : IComponent
{
    public Book(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public string Title { get; }
    public string Type => "Book";

    public int Count { get; } = 1;
}
