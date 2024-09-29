using CreateAccount.Repository.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Method to check if the company exists
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
}
