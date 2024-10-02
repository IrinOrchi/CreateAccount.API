using System.ComponentModel.DataAnnotations;

namespace CreateAccount.AggregateRoot.Entities
{
    public class OrgType
    {
        public int OrgTypeId { get; set; }
        public string OrgTypeName { get; set; }
        public int UserDefined { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public int IndustryId { get; set; }
    }
}
