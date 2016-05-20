using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Anatoli.ViewModels.PersonnelAcitvityModel
{
    public  class BaseActivityEventViewModel
    {
        public virtual string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
