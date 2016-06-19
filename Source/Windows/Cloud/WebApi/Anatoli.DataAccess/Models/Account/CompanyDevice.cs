using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class CompanyDevice : AnatoliBaseModel
    {
        [StringLength(100)]
        public string DeviceModel { get; set; }
        [StringLength(100)]
        public string MacAdress { get; set; }
        [StringLength(100)]
        public string IEMI { get; set; }
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
