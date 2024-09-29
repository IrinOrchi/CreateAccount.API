using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.AggregateRoot.Entities
{
    public class Location
    {
        [Key]
        public int L_ID { get; set; }
        public string L_Name { get; set; }
        public bool? OutsideBangladesh { get; set; }
        public string L_Type { get; set; }
        public int? ParentID { get; set; }
    }

}
