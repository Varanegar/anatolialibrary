using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models.Route
{
    public class CustomerArea : BaseModel
    {
        [ForeignKey("RegionArea")]
        public Guid RegionAreaId { get; set; }
        public virtual RegionArea RegionArea { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
