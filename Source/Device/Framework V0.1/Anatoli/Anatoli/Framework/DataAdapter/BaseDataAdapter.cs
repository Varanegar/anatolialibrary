using Anatoli.Framework.Helper;
using Anatoli.Framework.Model;
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
                        var response = AnatoliClient.GetInstance().WebClient.SendGetRequest<List<DataModel>>(
                            TokenType.AppToken,
                        remoteParameters.WebServiceEndpoint,
                        remoteParameters.Params.ToArray()
                        );
                        return response;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            if (localParameters != null)
            {
                try
                {
                    using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                    {
                        var command = connection.CreateCommand(localParameters.GetCommand());
                        var result = command.ExecuteQuery<DataModel>();
                        return result;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            throw new SyncPolicyHelper.SyncPolicyException();
        }
        public static DataModel GetItem(DBQuery localParameters, RemoteQuery remoteParameters)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(DataModel));
            DataModel data = null;
            try
            {
                if (policy == SYNC_POLICY.ForceOnline)
                {
                    data = CloudRead(remoteParameters);
                }
                else if (policy == SYNC_POLICY.OnlineIfConnected)
                {
                    if (AnatoliClient.GetInstance().WebClient.IsOnline() && remoteParameters != null)
                        data = CloudRead(remoteParameters);
                    else
                        data = LocalRead(localParameters);
                }
                else if (policy == SYNC_POLICY.Offline)
                {
                    data = LocalRead(localParameters);
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static DataModel LocalRead(DBQuery parameters)
        {
            try
            {
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    var query = connection.CreateCommand(parameters.GetCommand());

                    var qResult = query.ExecuteQuery<DataModel>();
                    if (qResult.Count > 0)
                    {
                        return qResult.First<DataModel>();
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        static DataModel CloudRead(RemoteQuery parameters)
        {
            try
            {
                var response = AnatoliClient.GetInstance().WebClient.SendGetRequest<DataModel>(
                    TokenType.UserToken,
                parameters.WebServiceEndpoint,
                parameters.Params.ToArray()
                );
                if (response != null)
                {
                    return response;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataModel CloudInsert(RemoteQuery query)
        {
            throw new NotImplementedException();
        }
        internal static List<DataModel> GetListStatic(DBQuery dbQuery, RemoteQuery remoteQuery)
        {
            SYNC_POLICY policy = SyncPolicyHelper.GetInstance().GetModelSyncPolicy(typeof(DataModel));
            if (policy == SYNC_POLICY.ForceOnline && remoteQuery == null)
            {
                throw new SyncPolicyHelper.SyncPolicyException();
            }
            if (policy == SYNC_POLICY.OnlineIfConnected && remoteQuery != null)
            {
                if (AnatoliClient.GetInstance().WebClient.IsOnline())
                {
                    try
                    {
                        var response = AnatoliClient.GetInstance().WebClient.SendGetRequest<List<DataModel>>(
                            remoteQuery.TokenType,
                        remoteQuery.WebServiceEndpoint,
                        remoteQuery.Params.ToArray()
                        );
                        return response;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            if (dbQuery != null)
            {
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    try
                    {
                        var result = new List<DataModel>();
                        var command = connection.CreateCommand(dbQuery.GetCommand());
                        result = command.ExecuteQuery<DataModel>();
                        return result;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            else
                throw new SyncPolicyHelper.SyncPolicyException();
        }
        internal static int UpdateItemStatic(DBQuery dbQuery, RemoteQuery remoteQuery)
        {
            if (dbQuery == null && remoteQuery == null)
            {
                throw new ArgumentNullException();
            }
            if (dbQuery != null)
            {
                return LocalUpdate(dbQuery);
            }
            return 0;
        }
        static int LocalUpdate(DBQuery dbQuery)
        {
            try
            {
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    var query = connection.CreateCommand(dbQuery.GetCommand());
                    var qResult = query.ExecuteNonQuery();
                    return qResult;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
