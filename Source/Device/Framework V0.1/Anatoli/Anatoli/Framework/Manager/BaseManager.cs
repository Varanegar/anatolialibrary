using Anatoli.Framework.AnatoliBase;
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
        where Data : BaseDataModel, new()
    {
        protected DataAdapter dataAdapter = null;

        protected BaseManager()
        {
            dataAdapter = new DataAdapter();
        }

        public bool IsIdValid(string id)
        {
            return dataAdapter.IsDataIDValid(id);
        }
        //protected DataList GetAll(string query)
        //{
        //    return dataAdapter.GetAll(query);
        //}
        public async Task<DataList> GetAllAsync(string localQuery, RemoteQueryParams remoteQueryParams)
        {
            return await Task.Run(() => { return dataAdapter.GetAll(localQuery, remoteQueryParams); });
        }
        protected Data GetById(string id, RemoteQueryParams parameters)
        {
            return dataAdapter.GetById(id, parameters);
        }
        public async Task<Data> GetByIdAsync(string id, RemoteQueryParams parameters)
        {
            return await Task.Run(() => { return dataAdapter.GetById(id, parameters); });
        }
    }
}
