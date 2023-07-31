namespace DomainLayers;

public class NegativeValueException : Exception
{
    public NegativeValueException(int amountToAddOrRemove)
        : base($"The amount to add or remove can't be negative. Provided: {amountToAddOrRemove}.")
    {

    }
}
