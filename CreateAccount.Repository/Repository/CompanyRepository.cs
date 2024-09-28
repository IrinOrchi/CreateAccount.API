using CreateAccount.Repository.Data;
using CreateAccount.Repository.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;



namespace CreateAccount.Repository.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsCompanyExistAsync(string companyName)
        {
            // Normalize the incoming company name
            var normalizedCompanyName = companyName.Replace(" Ltd", "").Replace(" Pvt", "").ToLower();

            // Update the LINQ query to use ToLower(), which can be translated to SQL
            return await _context.Companies
                .AnyAsync(c => c.Name.Replace(" Ltd", "").Replace(" Pvt", "").ToLower() == normalizedCompanyName);
        }

    }

}
