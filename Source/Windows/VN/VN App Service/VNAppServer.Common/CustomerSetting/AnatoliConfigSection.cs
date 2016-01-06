using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNAppServer.Common.CustomerSetting
{
    public class AnatoliConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("WebServices", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(AnatoliWebServiceConfgiCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public AnatoliWebServiceConfgiCollection WebServices
        {
            get
            {
                return (AnatoliWebServiceConfgiCollection)base["WebServices"];
            }
        }
    }
}
