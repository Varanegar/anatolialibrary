using Anatoli.Framework.Helper;
using Anatoli.Framework.Model;
using AnatoliLibrary.Anatoliclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.DataAdapter
{
    public class BaseDataAdapter<DataListTemplate, Data>
        where DataListTemplate : BaseListModel<Data>, new()
        where Data : BaseModel, new()
    {
        public DataListTemplate GetAll(params object[] queryParams)
        {
            return GetAll(new DataListTemplate(), queryParams);
        }

        public DataListTemplate GetAll(DataListTemplate dataList, params object[] queryParams)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(Data));
            return dataList;
        }

        public Data GetById(int id)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(Data));
            return new Data();
        }

        public bool IsDataIDValid(int ID)
        {
            
            return false;

        }

    }
}
