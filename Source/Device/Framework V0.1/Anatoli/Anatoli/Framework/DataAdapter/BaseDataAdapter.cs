using Anatoli.Framework.Helper;
using Anatoli.Framework.Model;
using Anatoli.Anatoliclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.App.Model.Product;
using Anatoli.Framework.AnatoliBase;

namespace Anatoli.Framework.DataAdapter
{
    public abstract class BaseDataAdapter<DataListTemplate, Data>
        where DataListTemplate : BaseListModel<Data>, new()
        where Data : BaseDataModel, new()
    {
        protected Data DataModel;
        public DataListTemplate GetAll(string localQuery, RemoteQueryParams parameters)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(Data));
            if (policy == SYNC_POLICY.ForceOnline && parameters == null)
            {
                throw new SyncPolicyHelper.SyncPolicyException();
            }
            if (policy == SYNC_POLICY.OnlineIfConnected && parameters != null)
            {
                if (AnatoliClient.GetInstance().WebClient.IsOnline())
                {
                    try
                    {
                        var response = AnatoliClient.GetInstance().WebClient.SendGetRequest<BaseListResult<Data>>(
                        parameters.Endpoint,
                        parameters.Parameters
                        );
                        if (response.metaInfo.Result)
                        {
                            var l = new DataListTemplate();
                            foreach (var item in response.items)
                            {
                                l.Add(item);
                            }
                            return l;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            try
            {
                var connection = AnatoliClient.GetInstance().DbClient.Connection;
                var command = connection.CreateCommand(localQuery);
                var result = command.ExecuteQuery<Data>();
                var list = new DataListTemplate();
                foreach (var item in result)
                {
                    list.Add(item);
                }
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Data GetById(string id)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(Data));
            DataModel = new Data();
            try
            {
                if (policy == SYNC_POLICY.ForceOnline)
                {
                    CloudUpdate();
                }
                else if (policy == SYNC_POLICY.OnlineIfConnected)
                {
                    if (AnatoliClient.GetInstance().WebClient.IsOnline())
                        CloudUpdate();
                    else
                        LocalUpdate();
                }
                else if (policy == SYNC_POLICY.Offline)
                {
                    LocalUpdate();
                }
                return DataModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public abstract bool IsDataIDValid(string ID);
        public abstract void LocalUpdate();
        public abstract void CloudUpdate();
    }
}
