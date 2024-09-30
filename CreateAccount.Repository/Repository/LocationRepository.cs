using CreateAccount.AggregateRoot.Entities;
using CreateAccount.DTO.DTOs;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<LocationResponseDTO>> GetLocationsAsync(string districtId, string outsideBd)
        {
            IQueryable<Location> query = _context.Locations;

            if (!string.IsNullOrEmpty(outsideBd) && outsideBd == "1")
            {
                query = query.Where(l => l.OutsideBangladesh == true);
            }
            else if (string.IsNullOrEmpty(districtId)) 
            {
                query = query.Where(l => l.OutsideBangladesh == false && l.L_Type == "District");
            }
            else
            {
               
                if (districtId == "0")
                {
        
                    query = query.Where(l => l.OutsideBangladesh == false && l.L_Type == "District");
                }
                else
                {
            
                    int districtIdInt = int.Parse(districtId); 
                    query = query.Where(l => l.OutsideBangladesh == false && l.L_Type == "thana" && l.ParentID == districtIdInt);
                }
            }

            query = query.OrderBy(l => l.L_Name);

            
            return await query
                .Select(l => new LocationResponseDTO
                {
                    OptionValue = l.L_ID.ToString(),
                    OptionText = l.L_Name
                })
                .ToListAsync();
        }

    }

}
