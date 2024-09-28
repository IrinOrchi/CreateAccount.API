using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.DTO.DTOs
{
    public class CheckNamesRequestDTO
    {
        public string UserName { get; set; }
        public string CheckFor { get; set; }
        public string CompanyName { get; set; }
    }

}
