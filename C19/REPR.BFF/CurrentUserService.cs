namespace REPR.BFF;

public interface ICurrentUserService
{
    int Id { get; }
}


public class FakeCurrentUserService : ICurrentUserService
{
    public int Id => 1;
}
