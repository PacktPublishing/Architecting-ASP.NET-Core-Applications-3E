namespace Factory.Models;

public class HomePageViewModel
{
    public IEnumerable<string> SomeData { get; }
    public HomePageViewModel(IEnumerable<string> someData)
    {
        SomeData = someData ?? throw new ArgumentNullException(nameof(someData));
    }
}
