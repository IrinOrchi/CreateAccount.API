using System.ComponentModel.DataAnnotations;

namespace CreateAccount.AggregateRoot.Entities
{
    public class IndustryWiseCompany
    {
        [Key]
        public int CorporateID { get; set; }
        public int OrgTypeId { get; set; }
    }
}
