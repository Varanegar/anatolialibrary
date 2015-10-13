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
    public class BaseDataAdapter<DataModel>
        where DataModel : BaseDataModel, new()
    {
        public List<DataModel> GetList(DBQuery localParameters)
        {
            return GetList(localParameters, null);
        }
        public List<DataModel> GetList(RemoteQuery remoteParameters)
        {
            return GetList(null, remoteParameters);
        }
        public List<DataModel> GetList(DBQuery localParameters, RemoteQuery remoteParameters)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(DataModel));
            if (policy == SYNC_POLICY.ForceOnline && remoteParameters == null)
            {
                throw new SyncPolicyHelper.SyncPolicyException();
            }
            if (policy == SYNC_POLICY.OnlineIfConnected && remoteParameters != null)
            {
                if (AnatoliClient.GetInstance().WebClient.IsOnline())
                {
                    try
                    {
                        var response = AnatoliClient.GetInstance().WebClient.SendGetRequest<BaseListResult<DataModel>>(
                        remoteParameters.WebServiceEndpoint,
                        remoteParameters.Params.ToArray()
                        );
                        if (response.metaInfo.Result)
                        {
                            var list = new List<DataModel>();
                            return list;
                        }
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            try
            {
                var connection = AnatoliClient.GetInstance().DbClient.Connection;
                var command = connection.CreateCommand(localParameters.Command);
                var result = command.ExecuteQuery<DataModel>();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataModel GetItem(DBQuery localParameters, RemoteQuery remoteParameters)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(DataModel));
            DataModel data = null;
            try
            {
                if (policy == SYNC_POLICY.ForceOnline)
                {
                    data = CloudUpdate(remoteParameters);
                }
                else if (policy == SYNC_POLICY.OnlineIfConnected)
                {
                    if (AnatoliClient.GetInstance().WebClient.IsOnline())
                        data = CloudUpdate(remoteParameters);
                    else
                        data = LocalUpdate(localParameters);
                }
                else if (policy == SYNC_POLICY.Offline)
                {
                    data = LocalUpdate(localParameters);
                }
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataModel LocalUpdate(DBQuery parameters)
        {
            var connection = AnatoliClient.GetInstance().DbClient.Connection;
            var query = connection.CreateCommand(parameters.Command);
            try
            {
                var qResult = query.ExecuteQuery<DataModel>();
                if (qResult.Count > 0)
                {
                    return qResult.First<DataModel>();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataModel CloudUpdate(RemoteQuery parameters)
        {
            try
            {
                var response = AnatoliClient.GetInstance().WebClient.SendGetRequest<BaseModelResult<DataModel>>(
                parameters.WebServiceEndpoint,
                parameters.Params.ToArray()
                );
                if (response.metaInfo.Result)
                {
                    return response.data;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
