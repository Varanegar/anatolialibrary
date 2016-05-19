using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.RequestModel
{
    public class ReportDataRequestModel
    {
        public string tblName { set; get; }
        public string valueName { set; get; }
        public string textName { set; get; }
        public string searchTrem { get; set; }

    }
}
