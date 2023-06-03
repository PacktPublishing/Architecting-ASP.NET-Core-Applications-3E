namespace DTO.Data.Models;

public record class Customer(
    int Id,
    string Name,
    List<Contract> Contracts
);

public record class Contract(
    int Id,
    string Name,
    string Description,
    ContractWork Work,
    ContactInformation PrimaryContact
);

public record class ContractWork(int Total, int Done)
{
    public WorkState State =>
        Done == 0 ? WorkState.New :
        Done == Total ? WorkState.Completed :
        WorkState.InProgress;
}

public record class ContactInformation(
    string Firstname,
    string Lastname,
    string Email
);

public enum WorkState
{
    New,
    InProgress,
    Completed
}
