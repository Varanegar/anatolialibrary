using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class CompanyOrgChart : BaseModel
    {

        [StringLength(200)]
        public string Title { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }

        [ForeignKey("ParentId")]
        public virtual CompanyOrgChart Parent { get; set; }
        public Guid? ParentId { get; set; }

        public virtual CompanyPersonnel CompanyPersonnel { get; set; }
        [ForeignKey("CompanyPersonnel")]
        public Guid? CompanyPersonnelId { get; set; }

        [ForeignKey("CompanyCenter")]
        public Guid CompanyCenterId { get; set; }
        public virtual CompanyCenter CompanyCenter { get; set; }
    }
}
