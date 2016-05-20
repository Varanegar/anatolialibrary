﻿using Anatoli.ViewModels.PersonnelAcitvityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.RequestModel
{
    public class PersonelTrackingRequestModel
    {
        public PersonnelDailyActivityEventViewModel activity { get; set; }
  
        public string date { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        
        public bool order{ get; set; }
        public bool lackOrder { get; set; }
        public bool lackVisit { get; set; }
        public bool stopWithoutCustomer { get; set; }
        public bool stopWithoutActivity { get; set; }

        public List<Guid> personelIds { get; set; }
    }
}
