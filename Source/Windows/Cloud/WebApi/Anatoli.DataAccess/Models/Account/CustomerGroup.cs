namespace Anatoli.DataAccess.Models
{
    using Common.DataAccess.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CustomerGroup : BaseModel
    {
        [StringLength(200)]
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }
        

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<CustomerGroup> CustomerGroup1 { get; set; }
        [ForeignKey("CustomerGroup2Id")]
        public virtual CustomerGroup CustomerGroup2 { get; set; }

        public Guid? CustomerGroup2Id { get; set; }
    }
}
