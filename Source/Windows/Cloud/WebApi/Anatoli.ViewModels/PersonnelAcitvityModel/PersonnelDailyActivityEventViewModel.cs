using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.PersonnelAcitvityModel
{
    public class PersonnelDailyActivityEventViewModel : PersonnelDailyActivityPointViewModel
    {
        
        public string CompanyPersonnelName { set; get; }
        public Guid CustomerId { get; set; }
        public string  CustomerName { get; set; }
        public Guid PersonnelDailyActivityVisitTypeId { get; set; }
        public string PersonnelDailyActivityVisitTypeName  { get; set; }
        public Guid PersonnelDailyActivityEventTypeId { get; set; }
        public string PersonnelDailyActivityEventTypeName { get; set; }
        public string ShortDescription { get; set; }

    }
}
