using System.Text.Json.Serialization;

namespace DTOs.Models;

public record class CustomerSummaryDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("totalNumberOfContracts")] int TotalNumberOfContracts,
    [property: JsonPropertyName("numberOfOpenContracts")] int NumberOfOpenContracts
);
/*
//
// The preceding record class is very similar to
// class commented next and plays the same role.
//
public class CustomerSummaryDto
{
    public CustomerSummaryDto(int id, string name, int totalNumberOfContracts, int numberOfOpenContracts)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        TotalNumberOfContracts = totalNumberOfContracts;
        NumberOfOpenContracts = numberOfOpenContracts;
    }

    [JsonPropertyName("id")]
    public int Id { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("totalNumberOfContracts")]
    public int TotalNumberOfContracts { get; }

    [JsonPropertyName("numberOfOpenContracts")]
    public int NumberOfOpenContracts { get; }
}
*/