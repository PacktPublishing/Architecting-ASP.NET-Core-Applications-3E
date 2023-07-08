using Microsoft.AspNetCore.Mvc;
using Strategy.Services;

namespace Strategy.Controllers;

[Route("travel/[controller]")]
[ApiController]
public class InjectImplementationLocationsController : ControllerBase
{
    private readonly InMemoryLocationService _locationService;
    public InjectImplementationLocationsController(InMemoryLocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<IEnumerable<LocationSummary>> GetAsync(CancellationToken cancellationToken)
    {
        var locations = await _locationService.FetchAllAsync(cancellationToken);
        return locations.Select(l => new LocationSummary(l.Id, l.Name));
    }
}
