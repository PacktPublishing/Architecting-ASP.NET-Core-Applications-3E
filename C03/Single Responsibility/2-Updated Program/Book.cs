namespace SRP;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
}

public class BookStore
{
    private static int _lastId = 0;

    private static List<Book> _books;
    public static int NextId => ++_lastId;

    static BookStore()
    {
        _books = new List<Book>
                {
                    new Book
                    {
                        Id = NextId,
                        Title = "Some cool computer book"
                    }
                };
    }

    public IEnumerable<Book> Books => _books;

    public void Save(Book book)
    {
        // Create the book when it does not exist, 
        // otherwise, find its index and replace it 
        // by the specified book.
        if (_books.Any(x => x.Id == book.Id))
        {
            var index = _books.FindIndex(x => x.Id == book.Id);
            _books[index] = book;
        }
        else
        {
            _books.Add(book);
        }
    }

    public Book? Load(int bookId)
    {
        return _books.FirstOrDefault(x => x.Id == bookId);
    }
}

public class BookPresenter
{
    public void Display(Book book)
    {
        Console.WriteLine($"Book: {book.Title} ({book.Id})");
    }
}
