namespace OperationResult.SimplestForm;

public class Executor
{
    public OperationResult Operation()
    {
        // Randomize the success indicator
        // This should be real logic
        var randomNumber = Random.Shared.Next(100);
        var success = randomNumber % 2 == 0;

        // Return the operation result
        return new OperationResult(success);
    }
}

public record class OperationResult(bool Succeeded);
