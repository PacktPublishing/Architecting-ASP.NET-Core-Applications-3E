using MySortingMachine;
using System.Text.Json;
using System.Text.Json.Serialization;

SortableCollection data = new(new[] { "Lorem", "ipsum", "dolor", "sit", "amet." });

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
var app = builder.Build();

app.MapGet("/", () => data);
app.MapPut("/", (ReplaceSortStrategy sortStrategy) =>
{
    ISortStrategy strategy = sortStrategy.SortOrder == SortOrder.Ascending
        ? new SortAscendingStrategy()
        : new SortDescendingStrategy();
    data.SetSortStrategy(strategy);
    data.Sort();
    return data;
});

app.Run();

public enum SortOrder
{
    Ascending,
    Descending
}

public record class ReplaceSortStrategy(SortOrder SortOrder);