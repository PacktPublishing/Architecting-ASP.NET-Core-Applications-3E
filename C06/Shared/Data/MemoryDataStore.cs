using Shared.Models;

namespace Shared.Data;
internal static class MemoryDataStore
{
    public static List<Customer> Customers { get; } = new();

    public static void Seed()
    {
        Customers.Add(new Customer(
            Id: 1,
            Name: "Jonny Boy Inc.",
            Contracts: new List<Contract>
            {
                new Contract(
                    Id: 1,
                    Name: "First contract",
                    Description: "This is the first contract.",
                    PrimaryContact: new ContactInformation(
                        FirstName: "John",
                        LastName: "Doe",
                        Email: "john.doe@jonnyboy.com"
                    ),
                    Status: new WorkStatus(
                        TotalWork: 100,
                        WorkDone: 100
                    )
                ),
                new Contract(
                    Id: 2,
                    Name: "Some other contract",
                    Description: "This is another contract.",
                    PrimaryContact: new ContactInformation(
                        FirstName: "Jane",
                        LastName: "Doe",
                        Email: "jane.doe@jonnyboy.com"
                    ),
                    Status: new WorkStatus(
                        TotalWork: 100,
                        WorkDone: 25
                    )
                )
            }
        ));
        Customers.Add(new Customer(
            Id: 2,
            Name: "Some mega-corporation",
            Contracts: new List<Contract>
            {
                new Contract(
                    Id: 3,
                    Name: "Huge contract",
                    Description: "This is a huge contract.",
                    PrimaryContact: new ContactInformation(
                        FirstName: "Kory",
                        LastName: "O'Neill",
                        Email: "kory.oneill@megacorp.com"
                    ),
                    Status: new WorkStatus(
                        TotalWork: 15000,
                        WorkDone: 0
                    )
                )
            }
        ));
    }
}
