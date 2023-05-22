namespace SRP;

public class Program
{
    private static readonly BookStore _bookStore = new();
    private static readonly BookPresenter _bookPresenter = new();

    public static void Main(string[] args)
    {
        var run = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Choices:");
            Console.WriteLine("1: Fetch and display book id 1");
            Console.WriteLine("2: Fail to fetch a book");
            Console.WriteLine("3: Book does not exist");
            Console.WriteLine("4: Create an out of order book");
            Console.WriteLine("5: Display a book somewhere else");
            Console.WriteLine("6: Create a book");
            Console.WriteLine("7: List all books");
            //...
            Console.WriteLine("0: Exit");

            var input = Console.ReadLine();
            Console.Clear();
            try
            {
                switch (input)
                {
                    case "1":
                        FetchAndDisplayBook();
                        break;
                    case "2":
                        FailToFetchBook();
                        break;
                    case "3":
                        BookDoesNotExist();
                        break;
                    case "4":
                        CreateOutOfOrderBook();
                        break;
                    case "5":
                        DisplayTheBookSomewhereElse();
                        break;
                    case "6":
                        CreateBook();
                        break;
                    case "7":
                        ListAllBooks();
                        break;
                    case "0":
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
                Console.WriteLine("Press enter to go back to the main menu.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception occurred, press enter to continue:");
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        } while (run);
    }

    private static void FetchAndDisplayBook()
    {
        var book = _bookStore.Load(1);
        _bookPresenter.Display(book!);
    }

    private static void FailToFetchBook()
    {
        // This cannot happen anymore,
        // this has been fixed automatically.
    }

    private static void BookDoesNotExist()
    {
        var book = _bookStore.Load(999);
        if (book == null)
        {
            // Book does not exist
        }
    }

    private static void CreateOutOfOrderBook()
    {
        var book = new Book
        {
            Id = 4,
            Title = "Some out of order book"
        };
        _bookStore.Add(book); // Exception: A new book cannot be created with an id.
        _bookPresenter.Display(book);
    }

    private static void DisplayTheBookSomewhereElse()
    {
        Console.WriteLine("This is now possible, but we need a new Presenter; not 100% there yet!");
    }

    private static void CreateBook()
    {
        Console.Clear();
        Console.WriteLine("Please enter the book title: ");
        var title = Console.ReadLine();
        var book = new Book { Title = title };
        _bookStore.Add(book);
    }

    private static void ListAllBooks()
    {
        foreach (var book in _bookStore.Books)
        {
            _bookPresenter.Display(book);
        }
    }
}
