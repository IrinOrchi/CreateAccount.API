using CreateAccount.DTO.DTOs;

public class LocationAggregateRoot
{
    public string DistrictId { get; private set; }
    public string OutsideBd { get; private set; }

    // Manual mapping of LocationRequestDTO to internal properties
    public LocationAggregateRoot(LocationRequestDTO dto)
    {
        DistrictId = !string.IsNullOrEmpty(dto.DistrictId) ? dto.DistrictId : null;
        OutsideBd = dto.OutsideBd;
    }

    // Additional domain logic can be added here if needed
}
