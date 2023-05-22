namespace ISP;

public class BookPresenter
{
    public void Display(Book book)
    {
        Console.WriteLine($"Book: {book.Title} ({book.Id})");
    }
}
