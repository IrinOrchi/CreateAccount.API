using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.DTO.DTOs
{
    public class IndustryTypeRequestDTO
    {
        public int? IndustryId { get; set; }
        public string OrganizationText { get; set; }
        public int? CorporateID { get; set; }
    }
}
