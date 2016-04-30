using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using TrackingMap.Common.ViewModel;

namespace TrackingMap.Common.ViewModel
{
   
    public class AreaCondition
    {
        public Guid Id{ set; get; }
        public bool Editable{ set; get; }
        public bool Showcust{ set; get; }
        public bool Showcustrout{ set; get; }
        public bool Showcustotherrout{ set; get; }
        public bool Showcustwithoutrout { set; get; }
    }

}