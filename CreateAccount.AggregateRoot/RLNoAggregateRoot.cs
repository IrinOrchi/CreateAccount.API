using CreateAccount.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.AggregateRoot
{
    public class RLNoAggregateRoot
    {
        public string RLNo { get; private set; }

        public RLNoAggregateRoot(RLNoRequestDTO dto)
        {
            RLNo = dto.RLNo;
        }

        // Validation method for RL_no
        public bool IsValid(out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validate RL_no is numeric and non-empty
            if (string.IsNullOrEmpty(RLNo) || !int.TryParse(RLNo, out _))
            {
                errorMessage = "RL_no is invalid or empty.";
                return false;
            }

            return true;
        }
    }

}
