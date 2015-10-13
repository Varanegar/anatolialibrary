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
    public abstract class BaseManager<DataAdapter, DataModel>
        where DataAdapter : BaseDataAdapter<DataModel>, new()
        where DataModel : BaseDataModel, new()
    {
        protected DataAdapter dataAdapter = null;
        int _limit = 10;
        protected DBQuery _localP;
        protected RemoteQuery _remoteP;
        protected string DataTable
        {
            get { return GetDataTable(); }
            set { SetDataTable(value); }
        }
        protected void SetDataTable(string value)
        {
            DataTable = value;
        }
        protected abstract string GetDataTable();


        protected string WebServiceUri
        {
            get { return GetWebServiceUri(); }
            set { SetWebServiceUri(value); }
        }
        protected void SetWebServiceUri(string value)
        {
            WebServiceUri = value;
        }
        protected abstract string GetWebServiceUri();


        public int Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }
        protected BaseManager()
        {
            dataAdapter = new DataAdapter();
        }

        public bool IsIdValid(string id)
        {
            return true;
        }
        public void SetQueryParameters(List<Query.QueryParameter> parameters)
        {
            SetRemoteQueryParameters(parameters);
            SetDBQueryParameters(parameters);
        }
        void SetRemoteQueryParameters(List<Query.QueryParameter> parameters)
        {
            _remoteP = new RemoteQuery(WebServiceUri, parameters);
        }
        void SetDBQueryParameters(List<Query.QueryParameter> parameters)
        {
            _localP = new DBQuery(DataTable, parameters);
        }
        public async Task<List<DataModel>> GetNextAsync()
        {
            if (_localP == null && _remoteP == null)
            {
                throw new ArgumentNullException("Both Local query and remote quesry are null. You have to call SetDBQuery() or SetRemoteQuery()");
            }
            _localP.Limit = _limit;
            _remoteP.Limit = _limit;
            var list = await Task.Run(() => { return dataAdapter.GetList(_localP, _remoteP); });
            _localP.Index += Math.Min(list.Count, _limit);
            _remoteP.Index += Math.Min(list.Count, _limit);
            return list;
        }
        public async Task<DataModel> GetByIdAsync(string id)
        {
            var localP = new DBQuery(DataTable, new Query.FilterParam("id", id));
            return await Task.Run(() => { return dataAdapter.GetItem(localP, null); });
        }
    }
}
