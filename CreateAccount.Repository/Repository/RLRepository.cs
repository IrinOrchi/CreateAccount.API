using CreateAccount.DTO.DTOs;
using CreateAccount.Repository.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.Repository.Repository
{
    public class RLRepository : IRLRepository
    {
        private readonly ApplicationDbContext _context;

        public RLRepository(ApplicationDbContext context)
        {
            _context = context;
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
