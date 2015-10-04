using Anatoli.Framework.Helper;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.DataAdapter
{
    public abstract class DataAdapter<DataListTemplate, Data> : BaseDataAdapter<DataListTemplate, Data>
        where DataListTemplate : BaseListModel<Data>, new()
        where Data : BaseDataModel, new()
    {
        public override bool IsDataIDValid(string ID)
        {
            return false;
        }
        public abstract void CloudSave();
        public abstract void LocalSave();
    }
}
