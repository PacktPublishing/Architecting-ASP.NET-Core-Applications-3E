using System.Diagnostics.CodeAnalysis;

namespace RegistrationApp;

public class ConcertRegistrationService
{
    public async Task<ConcertRegistrationResult> RegisterAsync(User user, Concert concert)
    {
        var (success, confirmationNumber) = await SimulatedRegistrationProcessAsync(user, concert);
        if (!success)
        {
            return ConcertRegistrationResult.CreateFailure(
                user,
                concert,
                "The registration to the concert failed."
            );
        }
        return ConcertRegistrationResult.CreateSuccess(
            user, concert, confirmationNumber);
    }

    private static async Task<(bool Success, string ConfirmationNumber)> SimulatedRegistrationProcessAsync(User user, Concert concert)
    {
        // Simulate an async operation
        await Task.Delay(Random.Shared.Next(10, 100));

        // Return a simulated result
        return (concert.Id == 1, Guid.NewGuid().ToString());
    }
}

public record class ConcertRegistrationResult
{
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    [MemberNotNullWhen(true, nameof(ConfirmationNumber))]
    public bool RegistrationSucceeded { get; init; }

    public User User { get; init; } = null!;
    //public required User User { get; init; } // Alternative

    public Concert Concert { get; init; } = null!;
    //public required Concert Concert { get; init; } // Alternative

    public string? ConfirmationNumber { get; init; }
    public string? ErrorMessage { get; init; }

    private ConcertRegistrationResult() { }

    public static ConcertRegistrationResult CreateSuccess(User user, Concert concert, string confirmationNumber)
    {
        return new()
        {
            RegistrationSucceeded = true,
            User = user,
            Concert = concert,
            ConfirmationNumber = confirmationNumber,
        };
    }

    public static ConcertRegistrationResult CreateFailure(User user, Concert concert, string errorMessage)
    {
        return new()
        {
            RegistrationSucceeded = false,
            User = user,
            Concert = concert,
            ErrorMessage = errorMessage,
        };
    }
}

public record class Concert(int Id, string Name);
public record class User(string Name);