using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.Repository.Repository.Abstraction
{
    public interface ICompanyRepository
    {
        Task<bool> IsCompanyExistAsync(string companyName);
    }
}
