using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Common.ViewModel
{
    public class AreaView
    {
        public Guid Id { set; get; }
        public string Title { set; get; }
        public bool IsLeaf { set; get; }

    }
}
