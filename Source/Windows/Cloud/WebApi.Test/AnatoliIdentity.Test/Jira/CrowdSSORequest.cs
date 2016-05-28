using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace App.Authorise
{
    public class CrowdSSORequest
    {
        public CrowdSSOAPICall apiCall { get; set; }
        public string method { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string group { get; set; }

    }
}