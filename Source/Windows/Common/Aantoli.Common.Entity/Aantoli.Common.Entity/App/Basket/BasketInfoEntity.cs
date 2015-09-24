using Aantoli.Common.Entity.Base;
using Aantoli.Common.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.App.Basket
{
    public class BasketInfoEntity : BaseEntity
    {
        public BasketInfoEntity()
        {
            
        }

        public string BasketName { get; set; }
        public string BasketComment { get; set; }
        public DateTime ChangeDate { get; set; }
        public SOURCE_TYPE RequestSourceId { get; set; }
        public string DeviceIMEI { get; set; }
        public List<String> NoteInfoList { get; set; }
        public List<BasketInfoEntity> ItemInfoList { get; set; }

    }
}
