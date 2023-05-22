namespace DTOs.Models;

public record class Contract(
    int Id,
    string Name,
    string Description,
    ContractWork Work,
    ContactInformation PrimaryContact
);
