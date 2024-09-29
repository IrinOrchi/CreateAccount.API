using Microsoft.AspNetCore.Mvc;
using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationHandler _locationHandler;

    public LocationController(ILocationHandler locationHandler)
    {
        _locationHandler = locationHandler;
    }

    [HttpPost("GetLocations")]
    public async Task<IActionResult> GetLocations([FromBody] LocationRequestDTO request)
    {
        // Call the handler to process the request and get locations
        var locations = await _locationHandler.Handle(request);

        // Check if there are no locations found
        if (locations.Count == 0)
        {
            return Ok(new { Error = "0", BdDistrict = "null" });
        }

        // Return the locations in the response
        return Ok(new
        {
            Error = "0",
            BdDistrict = locations
        });
    }
}
