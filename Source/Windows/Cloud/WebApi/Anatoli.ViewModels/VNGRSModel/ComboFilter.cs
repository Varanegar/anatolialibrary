using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Common.ViewModel
{
    public class ComboFilter
    {
        public string TblName { set; get; }
        public string ValueName { set; get; }
        public string TextName { set; get; }

        public bool AddEmptyRow { set; get; }

    }
}
