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
    private static int NextId => ++_lastId;

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

    public void Add(Book book)
    {
        if (book.Id != default)
        {
            throw new Exception("A new book cannot be created with an id.");
        }
        book.Id = NextId;
        _books.Add(book);
    }

    public void Replace(Book book)
    {
        if (!_books.Any(x => x.Id == book.Id))
        {
            throw new Exception($"Book {book.Id} does not exist!");
        }
        var index = _books.FindIndex(x => x.Id == book.Id);
        _books[index] = book;
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
