//using CreateAccount.AggregateRoot.Entities;
//using CreateAccount.DTO.DTOs;

//public class IndustryTypeAggregateRoot
//{
//    public OrgType IndustryType { get; private set; }

//    public IndustryTypeAggregateRoot(IndustryTypeRequestDTO dto)
//    {
//        if (dto.IndustryId.HasValue && dto.IndustryId >= -1)
//        {
//            IndustryType = new OrgType
//            {
//                IndustryId = dto.IndustryId.Value
//            };
//        }

//        if (!string.IsNullOrEmpty(dto.OrganizationText))
//        {
//            IndustryType.OrgTypeName = dto.OrganizationText.Trim().Replace("'", "`");
//        }

//        if (dto.CorporateID.HasValue && dto.CorporateID > 0)
//        {
//            IndustryType.CorporateID = dto.CorporateID.Value;
//        }
//    }
//}
