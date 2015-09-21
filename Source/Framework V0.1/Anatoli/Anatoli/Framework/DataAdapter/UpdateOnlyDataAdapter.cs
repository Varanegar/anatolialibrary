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
    public abstract class UpdateOnlyDataAdapter<DataListTemplate, Data> : BaseDataAdapter<DataListTemplate, Data>
        where DataListTemplate : BaseListModel<Data>, new()
        where Data : UpdateOnlyDataModel, new()
    {
        public override DataListTemplate GetAll(DataListTemplate dataList, params object[] queryParams)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(Data));
            return dataList;
        }

        public override Data GetById(int id)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(Data));
            Data data = new Data();
            if (policy == SYNC_POLICY.ForceOnline)
            {
                data.CloudUpdateAsync();
            }
            else if (policy == SYNC_POLICY.ForceOnlineIfConnected)
            {
                if (AnatoliClient.GetInstance().WebClient.IsOnline())
                {
                    data.CloudUpdateAsync();
                }
            }
            else if (policy == SYNC_POLICY.Offline)
            {
                data.LocalUpdateAsync();
            }
            return data;
        }

        public override bool IsDataIDValid(int ID)
        {
            return false;
        }
    }
}
