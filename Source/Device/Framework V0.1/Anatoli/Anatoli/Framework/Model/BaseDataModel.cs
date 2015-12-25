using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.Model
{
    public abstract class BaseDataModel
    {
        public BaseDataModel()
        {

        }
        public string Id { get; set; }
        public bool IsSaveRequired { get; set; }
        public bool ReadOnly { get { return false; } }
        public bool IsValid { get { return (String.IsNullOrEmpty(message)) ? true : false; } private set { IsValid = value; } }
        public string message { get; set; }
        public string ModelStateString
        {
            get
            {
                string str = "";
                if (ModelState != null)
                {
                    foreach (var item in ModelState)
                    {
                        foreach (var item2 in item.Value)
                        {
                            str += item2 + Environment.NewLine;
                        }
                    }
                }
                return str;
            }
        }
        public Dictionary<string, string[]> ModelState { get; set; }

    }
}
