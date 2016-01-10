using Anatoli.ViewModels.BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VNAppServer.Anatoli.Common
{
    public class ConnectionHelperRequestModel
    {
        public string privateOwnerId { get; set; }
        public string stockId { get; set; }
        public string dateAfter { get; set; }
        public string ruleDate { get; set; }
    }
}
