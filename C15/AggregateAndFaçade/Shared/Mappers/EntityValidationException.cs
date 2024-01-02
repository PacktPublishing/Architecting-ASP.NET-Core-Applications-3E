namespace Shared.Mappers;

public class EntityValidationException : Exception
{
    public EntityValidationException(string message) : base(message)
    {
    }
}
