using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNAppServer.Common.CustomerSetting
{
    public class AnatoliWebServiceConfgiCollection : ConfigurationElementCollection
    {
        public AnatoliWebServiceConfgiCollection()
        {
        }

        public AnatoliWebServiceConfig this[int index]
        {
            get { return (AnatoliWebServiceConfig)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(AnatoliWebServiceConfig serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AnatoliWebServiceConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AnatoliWebServiceConfig)element).WebServiceName;
        }

        public void Remove(AnatoliWebServiceConfig serviceConfig)
        {
            BaseRemove(serviceConfig.WebServiceName);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }
    }
}
