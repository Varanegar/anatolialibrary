using Anatoli.Framework.Helper;
using Anatoli.Framework.Model;
using Anatoli.Anatoliclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.DataAdapter
{
    public abstract class BaseDataAdapter<DataListTemplate, Data>
        where DataListTemplate : BaseListModel<Data>, new()
        where Data : BaseDataModel, new()
    {
        public DataListTemplate GetAll(params object[] queryParams)
        {
            return GetAll(new DataListTemplate(), queryParams);
        }

        public abstract DataListTemplate GetAll(DataListTemplate dataList, params object[] queryParams);

        public abstract Data GetById(int id);

        public abstract bool IsDataIDValid(int ID);

    }
}
