using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.AggregateRoot.Entities
{
    public class RecruitingAgency
    {
        [Key]
        public string RLNO { get; set; }
        public string RaStatus { get; set; }
        public string Name { get; set; }
    }
}
