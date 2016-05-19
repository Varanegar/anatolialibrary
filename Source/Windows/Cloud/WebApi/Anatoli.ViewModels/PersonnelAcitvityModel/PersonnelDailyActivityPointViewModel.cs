using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.PersonnelAcitvityModel
{
    public class PersonnelDailyActivityPointViewModel : BaseViewModel
    {
        public Guid CompanyPersonnelId { get; set; }
        public double Latitude { set; get; }
        public double Longitude { set; get; }
        public DateTime ActivityDate { get; set; }
        public string ActivityPDate { get; set; }

    }
}
