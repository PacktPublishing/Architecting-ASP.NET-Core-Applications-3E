namespace MVC.Models;

public record class Customer(
    int Id,
    string Name,
    List<Contract> Contracts
);

public record class Contract(
    int Id,
    string Name,
    string Description,
    WorkStatus Status,
    ContactInformation PrimaryContact
);

public record class WorkStatus(int TotalWork, int WorkDone)
{
    public WorkState State =>
        WorkDone == 0 ? WorkState.New :
        WorkDone == TotalWork ? WorkState.Completed :
        WorkState.InProgress;
}

public record class ContactInformation(
    string FirstName,
    string LastName,
    string Email
);

public enum WorkState
{
    New,
    InProgress,
    Completed
}
