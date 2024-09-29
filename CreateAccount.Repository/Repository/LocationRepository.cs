using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.Repository.Repository
{
    using Microsoft.EntityFrameworkCore;
    using CreateAccount.DTO.DTOs;
    using CreateAccount.Repository.Repository.Abstraction;

    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve locations based on the districtId and outsideBd flags
        public async Task<List<LocationResponseDTO>> GetLocationsAsync(string districtId, string outsideBd)
        {
            string query;

            if (!string.IsNullOrEmpty(outsideBd) && outsideBd == "1")
            {
                query = "SELECT L_ID, L_Name FROM Locations WHERE OutsideBangladesh = 1 ORDER BY L_Name";
            }
            else if (string.IsNullOrEmpty(districtId))
            {
                query = "SELECT L_ID, L_Name FROM Locations WHERE OutsideBangladesh = 0 and l_Type = 'District' ORDER BY L_Name";
            }
            else
            {
                query = $"SELECT L_ID, L_Name FROM Locations WHERE OutsideBangladesh = 0 and l_Type = 'thana' and ParentID = {districtId}";
            }

            // Execute the query and return the result
            return await _context.Locations
                .FromSqlRaw(query)
                .Select(l => new LocationResponseDTO
                {
                    OptionValue = l.L_ID.ToString(),
                    OptionText = l.L_Name
                }).ToListAsync();
        }
    }

}
