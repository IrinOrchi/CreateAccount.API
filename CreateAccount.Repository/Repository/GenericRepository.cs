using CreateAccount.AggregateRoot.Entities;
using CreateAccount.DTO.DTOs;
using CreateAccount.Repository.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        public async Task<List<IndustryTypeResponseDTO>> GetIndustryTypesAsync(int? industryId, string organizationText = null, int? corporateID = null)
        {
            var lIndustryType = new List<IndustryTypeResponseDTO>();

            try
            {
                var query = _context.OrgTypes
                    .Where(o => o.UserDefined == 0 || (o.UserDefined > 0 && o.VerifiedOn != null))
                    .AsQueryable();

                if (industryId.HasValue && industryId.Value > 0)
                {
                    query = query.Where(o => o.IndustryId == industryId);
                }

                if (!string.IsNullOrEmpty(organizationText))
                {
                    query = query.Where(o => o.OrgTypeName.Contains(organizationText));
                }

                var lOrgTypes = await query.OrderBy(o => o.OrgTypeName).ToListAsync();

                if (lOrgTypes.Any())
                {
                    foreach (var orgType in lOrgTypes)
                    {
                        lIndustryType.Add(new IndustryTypeResponseDTO
                        {
                            IndustryValue = orgType.OrgTypeId,
                            IndustryName = orgType.OrgTypeName
                        });
                    }
                }
                else
                {

                    lIndustryType.Add(new IndustryTypeResponseDTO { IndustryValue = -1, IndustryName = "null" });
                }
            }
            catch (Exception ex)
            {

                throw; 
            }

            return lIndustryType;
        }


        public async Task<RLNoDataDTO> GetRLNoDataAsync(string rlNo)
        {
            // LINQ Query to fetch RLNo data
            var data = await (from ra in _context.RecruitingAgencies
                              where ra.RLNO == rlNo
                              select new RLNoDataDTO
                              {
                                  RL_no = ra.RaStatus,
                                  Company_Name = ra.Name
                              }).FirstOrDefaultAsync();

            return data;
        }

    }
}




