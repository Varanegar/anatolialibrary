using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.anatoliaclient
{
    public class Configuration
    {
        public static readonly string parseAppId = "ecg0bl83b3s1B57NtV65iGiy3IH38QfavsF1DHeX";
        public static readonly string parseDotNetKey = "SmgvPBYprBhYo1KTGPIhjoevR3YhXBccqFwqvfXL";
        public static readonly string PortalUri = "https://anatolia.varanegar.com";

        public static string RateProductUri { get { return Path.Combine(PortalUri, "RateProduct"); } }
    }
}
