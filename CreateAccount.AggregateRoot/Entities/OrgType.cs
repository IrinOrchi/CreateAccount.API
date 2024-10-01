using System.ComponentModel.DataAnnotations;

namespace CreateAccount.AggregateRoot.Entities
{
    public class OrgType
    {
        [Key]
        public int OrgTypeId { get; set; }

        [Required]
        [MaxLength(255)]
        public string OrgTypeName { get; set; }

        public int IndustryId { get; set; }

        public bool UserDefined { get; set; }

        public DateTime? VerifiedOn { get; set; }
    }
}
