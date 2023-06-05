namespace Shared.DTO;

public record class ContractDetails(
    int Id,
    string Name,
    string Description,
    int TotalWork,
    int WorkDone,
    string WorkStatus,
    string PrimaryContactFirstName,
    string PrimaryContactLastName,
    string PrimaryContactEmail
);
public record class CustomerDetails(
    int Id,
    string Name,
    IEnumerable<ContractDetails> Contracts
);
public record class CustomerSummary(
    int Id,
    string Name,
    int TotalNumberOfContracts,
    int NumberOfOpenContracts
);