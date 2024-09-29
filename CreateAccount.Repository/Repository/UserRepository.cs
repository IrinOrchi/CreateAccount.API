using CreateAccount.Repository.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CreateAccount.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }
    }

}
