var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();

app.MapGet(
    "/{id:int}",
    (int id) => new ReadOneDto(
        id,
        "John Doe"
    )
);
app.MapPost(
    "/",
    (CreateDto input) => new CreatedDto(
        Random.Shared.Next(int.MaxValue),
        input.Name
    )
);

app.Run();

public record class ReadOneDto(int Id, string Name);
public record class CreateDto(string Name);
public record class CreatedDto(int Id, string Name);
