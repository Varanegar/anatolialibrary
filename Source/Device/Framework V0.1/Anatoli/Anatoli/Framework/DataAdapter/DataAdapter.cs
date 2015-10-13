using Anatoli.Framework.Helper;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.DataAdapter
{
    public abstract class DataAdapter<Data> : BaseDataAdapter<Data>
        where Data : BaseDataModel, new()
    {
        public abstract void CloudSave();
        public abstract void LocalSave();
    }
}
