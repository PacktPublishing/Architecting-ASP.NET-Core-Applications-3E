var current = new Forecast(DateTime.UtcNow, 20, "Sunny");
var (date, temperatureC, summary, temperatureF) = current;

Console.WriteLine($"date: {date}");
Console.WriteLine($"temperatureC: {temperatureC}");
Console.WriteLine($"summary: {summary}");
Console.WriteLine($"temperatureF: {temperatureF}");

public record class Forecast(DateTime Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public void Deconstruct(out DateTime date, out int temperatureC, out string summary, out int temperatureF) 
      => (date, temperatureC, summary, temperatureF) = (Date, TemperatureC, Summary, TemperatureF);
}
