using DTOs.Models;

namespace DTOs.Services;

public class CustomerRepository
{
    public IEnumerable<Customer> ReadAll()
    {
        yield return new Customer(
            Id: 1,
            Name: "Jonny Boy Inc.",
            Contracts: new List<Contract>
            {
                new Contract(
                    Id: 1,
                    Name: "First contract",
                    Description: "This is the first contract of Jonny Boy Inc.",
                    PrimaryContact: new ContactInformation(
                        Firstname: "John",
                        Lastname: "Doe",
                        Email: "john.doe@jonnyboy.com"
                    ),
                    Work: new ContractWork(
                        Total: 100,
                        Done: 100
                    )
                ),
                new Contract(
                    Id: 2,
                    Name: "Some other contract",
                    Description: "This is another contract of Jonny Boy Inc.",
                    PrimaryContact: new ContactInformation(
                        Firstname: "Jane",
                        Lastname: "Doe",
                        Email: "jane.doe@jonnyboy.com"
                    ),
                    Work: new ContractWork(
                        Total: 100,
                        Done: 25
                    )
                )
            }
        );
        yield return new Customer(
            Id: 2,
            Name: "Some mega-corporation",
            Contracts: new List<Contract>
            {
                new Contract(
                    Id: 3,
                    Name: "Huge contract",
                    Description: "This is a huge contract of Some mega-corporation.",
                    PrimaryContact: new ContactInformation(
                        Firstname: "Kory",
                        Lastname: "O'Neill",
                        Email: "kory.oneill@megacorp.com"
                    ),
                    Work: new ContractWork(
                        Total: 15000,
                        Done: 0
                    )
                )
            }
        );
    }

    public Customer? ReadOne(int id)
    {
        return ReadAll().FirstOrDefault(x => x.Id == id);
    }
}
