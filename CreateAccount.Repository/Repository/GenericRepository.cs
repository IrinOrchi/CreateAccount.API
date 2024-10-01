using CreateAccount.AggregateRoot.Entities;
using CreateAccount.DTO.DTOs;
using CreateAccount.Repository.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CreateAccount.Repository.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<bool> IsCompanyExistAsync(string companyName)
        {
            var normalizedCompanyName = companyName
                .Replace(" Limited", "")
                .Replace(" Ltd.", "")
                .Replace(" Ltd", "")
                .Replace(" Private", "")
                .Replace(" Pvt.", "")
                .Replace("(Pvt.)", "")
                .Replace(" Pvt", "")
                .Replace("(Pvt)", "")
                .Replace(" company", "")
                .Replace(" Co.", "")
                .Replace("(Co.)", "")
                .Replace(".", "")
                .Replace(" ", "")
                .Replace("(", "")
                .Replace(")", "")
                .ToLower();

            return await _context.Companies
                .AnyAsync(c => c.Name
                    .Replace(" Limited", "")
                    .Replace(" Ltd.", "")
                    .Replace(" Ltd", "")
                    .Replace(" Private", "")
                    .Replace(" Pvt.", "")
                    .Replace("(Pvt.)", "")
                    .Replace(" Pvt", "")
                    .Replace("(Pvt)", "")
                    .Replace(" company", "")
                    .Replace(" Co.", "")
                    .Replace("(Co.)", "")
                    .Replace(".", "")
                    .Replace(" ", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .ToLower() == normalizedCompanyName);
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

        public async Task<List<IndustryTypeResponseDTO>> GetIndustryTypeAsync(int? industryId, string organizationText, int? corporateID)
        {
            // Base query that includes all org types
            var query = _context.OrgTypes.AsQueryable();

       
                query = from c in _context.IndustryWiseCompanies
                        join o in query on c.OrgTypeId equals o.OrgTypeId
                        where c.CorporateID == corporateID
                        select o;
            

            // If IndustryId is provided, filter by IndustryId
            if (industryId.HasValue && industryId.Value != -1)
            {
                query = query.Where(o => o.IndustryId == industryId);
            }
            9
            // If OrganizationText is provided, filter by OrgTypeName containing the text
            if (!string.IsNullOrEmpty(organizationText))
            {
                query = query.Where(o => o.OrgTypeName.Contains(organizationText));
            }

            // Final result ordering and selection
            var orgTypes = await query.OrderBy(o => o.OrgTypeName)
                .Select(o => new IndustryTypeResponseDTO
                {
                    IndustryValue = o.OrgTypeId,
                    IndustryName = o.OrgTypeName
                }).ToListAsync();

            return orgTypes;
        }


    }

}
