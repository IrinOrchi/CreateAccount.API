using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using CreateAccount.Repository.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.Handler.Service
{
    public class LocationHandler : ILocationHandler
    {
        private readonly ILocationRepository _locationRepository;

        public LocationHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<List<LocationResponseDTO>> Handle(LocationRequestDTO request)
        {
            var locations = await _locationRepository.GetLocationsAsync(request.DistrictId, request.OutsideBd);
            return locations;
        }
    }
}
