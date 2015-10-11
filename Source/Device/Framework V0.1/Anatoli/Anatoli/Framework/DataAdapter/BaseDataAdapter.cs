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
    public class BaseDataAdapter<DataListTemplate, Data>
        where DataListTemplate : BaseListModel<Data>, new()
        where Data : BaseDataModel, new()
    {
        protected Data DataModel = new Data();
        public DataListTemplate GetAll()
        {
            return GetAll(string.Format("SELECT * FROM {0}", DataModel.DataTable), null);
        }
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
        public Data GetById(string id, RemoteQueryParams parameters)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(Data));
            try
            {
                if (policy == SYNC_POLICY.ForceOnline)
                {
                    CloudUpdate(parameters);
                }
                else if (policy == SYNC_POLICY.OnlineIfConnected)
                {
                    if (AnatoliClient.GetInstance().WebClient.IsOnline())
                        CloudUpdate(parameters);
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
        public bool IsDataIDValid(string ID)
        {
            return true;
        }
        public void LocalUpdate()
        {
            var connection = AnatoliClient.GetInstance().DbClient.Connection;
            var query = connection.CreateCommand(string.Format("SELECT * FROM {0} WHERE store_id={1}", DataModel.DataTable, DataModel.ID));
            try
            {
                var qResult = query.ExecuteQuery<Data>();
                if (qResult.Count > 0)
                {
                    DataModel = qResult.First<Data>();
                }
            }
            catch (Exception)
            {
            }
        }
        public void CloudUpdate(RemoteQueryParams parameters)
        {
            try
            {
                var response = AnatoliClient.GetInstance().WebClient.SendGetRequest<BaseModelResult<Data>>(
                parameters.Endpoint,
                new Tuple<string, string>("ID", DataModel.UniqueId.ToString())
                );
                if (response.metaInfo.Result)
                {
                    DataModel = response.data;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
