namespace Strategy.Services;

public interface IHomeService
{
    IEnumerable<string> GetHomePageData();
}

public class HomeService : IHomeService
{
    public IEnumerable<string> GetHomePageData()
    {
        yield return "Lorem";
        yield return "ipsum";
        yield return "dolor";
        yield return "sit";
        yield return "amet";
    }
}
