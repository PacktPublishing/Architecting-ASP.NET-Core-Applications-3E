using Microsoft.AspNetCore.Mvc;
using Factory.Services;

namespace Factory.Controllers;

[Route("travel/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;
    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<IEnumerable<LocationSummary>> GetAsync(CancellationToken cancellationToken)
    {
        var locations = await _locationService.FetchAllAsync(cancellationToken);
        return locations.Select(l => new LocationSummary(l.Id, l.Name));
    }
    public record class LocationSummary(int Id, string Name);
}
