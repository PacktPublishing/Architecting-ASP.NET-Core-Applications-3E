namespace SRP;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }

    private static int _lastId = 0;

    public static List<Book> Books { get; }
    public static int NextId => ++_lastId;

    static Book()
    {
        Books = new List<Book>
            {
                new Book
                {
                    Id = NextId,
                    Title = "Some cool computer book"
                }
            };
    }

    public Book(int? id = null)
    {
        Id = id ?? default;
    }

    public void Save()
    {
        // Create the book when it does not exist, 
        // otherwise, find its index and replace it 
        // by the current object.
        if (Books.Any(x => x.Id == Id))
        {
            var index = Books.FindIndex(x => x.Id == Id);
            Books[index] = this;
        }
        else
        {
            Books.Add(this);
        }
    }

    public void Load()
    {
        // Validate that an Id is set
        if (Id == default)
        {
            throw new Exception("You must set the Id to the Book Id you want to load.");
        }

        // Get the book
        var book = Books.FirstOrDefault(x => x.Id == Id);

        // Make sure it exist
        if (book == null)
        {
            throw new Exception("This book does not exist.");
        }

        // Copy the book properties to the current object
        Id = book.Id; // this should already be set
        Title = book.Title;
    }

    public void Display()
    {
        Console.WriteLine($"Book: {Title} ({Id})");
    }
}
