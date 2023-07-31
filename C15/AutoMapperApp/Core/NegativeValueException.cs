namespace Core;

public class NegativeValueException : Exception
{
    public NegativeValueException(int amountToAddOrRevove)
        : base($"The amount to add or remove can't be negative. Provided: {amountToAddOrRevove}.")
    {

    }
}
