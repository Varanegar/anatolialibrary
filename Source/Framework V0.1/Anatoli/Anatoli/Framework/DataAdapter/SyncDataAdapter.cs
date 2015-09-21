using Anatoli.Framework.Helper;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.DataAdapter
{
    public abstract class SyncDataAdapter<DataListTemplate, Data> : BaseDataAdapter<DataListTemplate, Data>
        where DataListTemplate : BaseListModel<Data>, new()
        where Data : SyncDataModel, new()
    {
        public override bool IsDataIDValid(int ID)
        {
            return false;
        }

        public override DataListTemplate GetAll(DataListTemplate dataList, params object[] queryParams)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(Data));
            return dataList;
        }

        public override Data GetById(int id)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(Data));
            var data = new Data();
            if (policy == SYNC_POLICY.ForceOnline)
            {
                data.CloudUpdateAsync();
            }
            return data;
        }
    }
}
