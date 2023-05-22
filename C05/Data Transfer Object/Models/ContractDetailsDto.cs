using System.Text.Json.Serialization;

namespace DTOs.Models;
public record class ContractDetailsDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("workTotal")] int WorkTotal,
    [property: JsonPropertyName("workDone")] int WorkDone,
    [property: JsonPropertyName("workState"), JsonConverter(typeof(JsonStringEnumConverter))] WorkState WorkState,
    [property: JsonPropertyName("primaryContactFirstname")] string PrimaryContactFirstname,
    [property: JsonPropertyName("primaryContactLastname")] string PrimaryContactLastname,
    [property: JsonPropertyName("primaryContactEmail")] string PrimaryContactEmail
);
/*
//
// The preceding record class is very similar to the
// class commented next and could play the same role.
//
public class ContractDetailsDto
{
    public ContractDetailsDto(int id, string name, string description, int workTotal, int workDone, WorkState workState, string primaryContactFirstname, string primaryContactLastname, string primaryContactEmail)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        WorkTotal = workTotal;
        WorkDone = workDone;
        WorkState = workState;
        PrimaryContactFirstname = primaryContactFirstname ?? throw new ArgumentNullException(nameof(primaryContactFirstname));
        PrimaryContactLastname = primaryContactLastname ?? throw new ArgumentNullException(nameof(primaryContactLastname));
        PrimaryContactEmail = primaryContactEmail ?? throw new ArgumentNullException(nameof(primaryContactEmail));
    }

    [JsonPropertyName("id")]
    public int Id { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("description")]
    public string Description { get; }

    [JsonPropertyName("workTotal")]
    public int WorkTotal { get; }

    [JsonPropertyName("workDone")]
    public int WorkDone { get; }

    [JsonPropertyName("workState")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WorkState WorkState { get; }

    [JsonPropertyName("primaryContactFirstname")]
    public string PrimaryContactFirstname { get; }

    [JsonPropertyName("primaryContactLastname")]
    public string PrimaryContactLastname { get; }

    [JsonPropertyName("primaryContactEmail")]
    public string PrimaryContactEmail { get; }
}
*/