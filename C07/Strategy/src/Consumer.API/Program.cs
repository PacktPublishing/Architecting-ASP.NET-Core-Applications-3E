using MySortingMachine;

SortableCollection data = new(new[] { "Lorem", "ipsum", "dolor", "sit", "amet." });

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => data);
app.MapPut("/{sortOrder}", (SortOrder sortOrder) =>
{
    data.SortStrategy = sortOrder == SortOrder.Ascending
        ? new SortAscendingStrategy()
        : new SortDescendingStrategy();
    data.Sort();
    return data;
});

app.Run();

public enum SortOrder
{
    Ascending,
    Descending
}