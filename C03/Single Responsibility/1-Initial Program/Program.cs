namespace SRP;

public class Program
{
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
                Console.WriteLine("The following exception occured, press enter to continue:");
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        } while (run);
    }

    private static void FetchAndDisplayBook()
    {
        var book = new Book(id: 1);
        book.Load();
        book.Display();
    }

    private static void FailToFetchBook()
    {
        var book = new Book();
        book.Load(); // Exception: You must set the Id to the Book Id you want to load.
        book.Display();
    }

    private static void BookDoesNotExist()
    {
        var book = new Book(id: 999);
        book.Load();
        book.Display();
    }

    private static void CreateOutOfOrderBook()
    {
        var book = new Book
        {
            Id = 4, // this value is not enforced by anything and will be overriden at some point.
            Title = "Some out of order book"
        };
        book.Save();
        book.Display();
    }

    private static void DisplayTheBookSomewhereElse()
    {
        Console.WriteLine("Oops! Can't do that, the Display method only write to the \"Console\".");
    }


    private static void CreateBook()
    {
        Console.Clear();
        Console.WriteLine("Please enter the book title: ");
        var title = Console.ReadLine();
        var book = new Book { Id = Book.NextId, Title = title };
        book.Save();
    }

    private static void ListAllBooks()
    {
        foreach (var book in Book.Books)
        {
            book.Display();
        }
    }
}
