using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Framework.Entity.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            UniqueId = Guid.NewGuid();
        }

        public int ID { get; set; }
        public Guid UniqueId { get; set; }
    }
}
