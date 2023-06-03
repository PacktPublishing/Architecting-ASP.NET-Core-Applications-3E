using System.Text.Json.Serialization;

namespace DTOs.Models;

public record class CustomerDetailsDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("contracts")] IEnumerable<ContractDetailsDto> Contracts
);
/*
//
// The preceding record class is very similar to
// class commented next and plays the same role.
//
public class CustomerDetailsDto
{
    public CustomerDetailsDto(int id, string name, IEnumerable<ContractDetailsDto> contracts)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Contracts = contracts ?? throw new ArgumentNullException(nameof(contracts));
    }

    [JsonPropertyName("id")]
    public int Id { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("contracts")]
    public IEnumerable<ContractDetailsDto> Contracts { get; }
}
*/