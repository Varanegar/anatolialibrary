using Aantoli.Framework.Entity.Base;
using Anatoli.Framework.DataAdapter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.Busieness
{
    public class BaseHandler<DataAdapter, DataList, Data>
        where DataAdapter : BaseDataAdapter<DataList, Data>, new()
        where DataList : BaseListEntity<Data>, new()
        where Data : BaseEntity, new()
    {
        protected DataAdapter dataAdapter = null;

        protected BaseHandler()
        {
            dataAdapter = new DataAdapter();
        }

        public bool IsIdValid(int id)
        {
            return dataAdapter.IsDataIDValid(id);
        }

        public DataList GetAll(params  object[] queryParams)
        {
            return dataAdapter.GetAll(queryParams);
        }

        public Data GetById(int id)
        {
            return dataAdapter.GetById(id);
        }

        public string GetAllJson(params  object[] queryParams)
        {
            DataList data = GetAll(queryParams);

            return JsonConvert.SerializeObject(data); ;
        }
    }
}
