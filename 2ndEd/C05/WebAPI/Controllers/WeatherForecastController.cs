using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
        => GetWeatherForecasts();

    [HttpGet("GenericClassActionDirect")]
    public ActionResult<IEnumerable<WeatherForecast>> GenericClassActionDirect()
        => GetWeatherForecasts();

    [HttpGet("GenericClassAction")]
    public ActionResult<IEnumerable<WeatherForecast>> GenericClassActionOk()
        => Ok(GetWeatherForecasts());

    [HttpGet("GenericClassActionNotFound")]
    public ActionResult<IEnumerable<WeatherForecast>> GenericClassActionNotFound()
        => NotFound();

    [HttpGet("InterfaceAction")]
    [ProducesResponseType(typeof(WeatherForecast[]), StatusCodes.Status200OK)]
    public IActionResult InterfaceAction()
        => Ok(GetWeatherForecasts());

    [HttpGet("ClassAction")]
    [ProducesResponseType(typeof(WeatherForecast[]), StatusCodes.Status200OK)]
    public ActionResult ClassAction()
        => Ok(GetWeatherForecasts());

    private static WeatherForecast[] GetWeatherForecasts()
        => Enumerable.Range(1, 5)
            .Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _summaries[Random.Shared.Next(_summaries.Length)]
            })
            .ToArray();
}
