using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.RequestModel
{
    public class BaseRequestModel
    {
        public Guid uniqueId { get; set; }
        public string data { get; set; }
        public string dateAfter { get; set; }
        public string searchTerm { get; set; }
        public string userId { get; set; }
    }
}
