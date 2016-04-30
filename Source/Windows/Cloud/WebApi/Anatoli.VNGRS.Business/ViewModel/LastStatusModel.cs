
using System;
using System.Collections.Generic;
using TrackingMap.Common.ViewModel;


namespace TrackingMap.Common.ViewModel
{


    public class LastStatusCondition
    {
        public LastStatusCondition()
        {
            VisitorIds = new List<Guid>();
        }
        public List<Guid> VisitorIds { get; set; }
    }
}