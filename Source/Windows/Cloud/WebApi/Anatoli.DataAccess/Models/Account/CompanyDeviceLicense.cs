using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class CompanyDeviceLicense : BaseModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("CompanyDevice")]
        public Guid CompanyDeviceId { get; set; }
        public virtual CompanyDevice CompanyDevice { get; set; }

    }
}
