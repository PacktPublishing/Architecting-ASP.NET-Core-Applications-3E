using DIP.Data;

namespace DIP.Core;

public class PublicService
{
    private readonly IBookReader _bookReader;
    public PublicService(IBookReader bookReader)
    {
        _bookReader = bookReader;
    }

    public Task<IEnumerable<Book>> FindAllAsync()
    {
        return Task.FromResult(_bookReader.Books);
    }

    public Task<Book?> FindAsync(int bookId)
    {
        var book = _bookReader.Find(bookId);
        return Task.FromResult(book);
    }
}
