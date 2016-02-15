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
        string _uniqueId;
        public string UniqueId { get { return _uniqueId.ToUpper(); } set { _uniqueId = value.ToUpper(); } }
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

        public string PrivateOwnerId { get { return "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"; } }
    }
}
