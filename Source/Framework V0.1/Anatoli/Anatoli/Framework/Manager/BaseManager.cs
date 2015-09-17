using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.Manager
{
    public class BaseManager<DataAdapter, DataList, Data>
        where DataAdapter : BaseDataAdapter<DataList, Data>, new()
        where DataList : BaseListModel<Data>, new()
        where Data : BaseModel, new()
    {
        protected DataAdapter dataAdapter = null;

        protected BaseManager()
        {
            dataAdapter = new DataAdapter();
        }

        public bool IsIdValid(int id)
        {
            return dataAdapter.IsDataIDValid(id);
        }

        public DataList GetAll()
        {
            return dataAdapter.GetAll();
        }

        public Data GetById(int id)
        {
            return dataAdapter.GetById(id);
        }
    }
}
