using SimpleEndpoint;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ShuffleText.Endpoint>();
builder.Services.AddSingleton<RandomNumber.Handler>();
builder.Services.AddUpperCase();

var app = builder.Build();

app.MapGet("/shuffle-text/{text}", ([AsParameters] ShuffleText.Request query, ShuffleText.Endpoint endpoint)
     => endpoint.Handle(query));

app.MapGet("/random-number/{Amount}/{Min}/{Max}", RandomNumber.Endpoint);

app.MapUpperCase();

app.Run();
