using System.ComponentModel.DataAnnotations;

namespace CreateAccount.AggregateRoot.Entities
{
    public class IndustryWiseCompany
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CorporateID { get; set; }

        [Required]
        public int OrgTypeId { get; set; }

        // Assuming this is a foreign key relationship to OrgType
        public OrgType OrgType { get; set; }
    }
}
