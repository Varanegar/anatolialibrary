using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    public class Query
    {
        public List<FilterParam> Filters;
        public List<SortParam> Sorts;
        public int Limit;
        public int Index;
        public Query(params QueryParameter[] options)
        {
            Filters = new List<FilterParam>();
            Sorts = new List<SortParam>();
            Index = 0;
            Limit = 10;
            foreach (var item in options)
            {
                if (item.GetType() == typeof(FilterParam))
                    Filters.Add(item as FilterParam);
                else if (item.GetType() == typeof(SortParam))
                    Sorts.Add(item as SortParam);
            }
        }
        public Query(List<QueryParameter> options)
        {
            Filters = new List<FilterParam>();
            Sorts = new List<SortParam>();
            Index = 0;
            Limit = 10;
            foreach (var item in options)
            {
                if (item.GetType() == typeof(FilterParam))
                    Filters.Add(item as FilterParam);
                else if (item.GetType() == typeof(SortParam))
                    Sorts.Add(item as SortParam);
            }
        }
        public abstract class QueryParameter
        {

        }
        public class FilterParam : QueryParameter
        {
            string _filterName;
            string _filterValue;
            public string FilterName { get { return _filterName; } }
            public string FilterValue { get { return _filterValue; } }
            public FilterParam(string filterName, string filterValue)
            {
                _filterName = filterName;
                _filterValue = filterValue;
            }
        }
        public class SortParam : QueryParameter
        {
            string _fieldName;
            SortTypes _sortType;
            public string FieldName { get { return _fieldName; } }
            public SortTypes SortType { get { return _sortType; } }
            public SortParam(string fieldName, SortTypes sortType)
            {
                _fieldName = fieldName;
                _sortType = sortType;
            }
        }
    }

    public class RemoteQuery : Query
    {
        public RemoteQuery(string WebServiceEndpoint, params QueryParameter[] options)
            : base(options)
        {
            this.WebServiceEndpoint = WebServiceEndpoint;
        }
        public RemoteQuery(string WebServiceEndpoint, List<QueryParameter> options)
            : base(options)
        {
            this.WebServiceEndpoint = WebServiceEndpoint;
        }
        public string WebServiceEndpoint { get; set; }
        public List<Tuple<string, string>> Params
        {
            get
            {
                List<Tuple<string, string>> parameters = new List<Tuple<string, string>>();
                foreach (var item in Filters)
                {
                    parameters.Add(new Tuple<string, string>(item.FilterName, item.FilterValue));
                }
                return parameters;
            }
        }
    }
    public class DBQuery : Query
    {
        public DBQuery(string DBTableName, params QueryParameter[] options)
            : base(options)
        {
            this.DBTableName = DBTableName;
        }
        public DBQuery(string DBTableName, List<QueryParameter> options)
            : base(options)
        {
            this.DBTableName = DBTableName;
        }
        public string DBTableName { get; set; }
        public string Command
        {
            get
            {
                string q = string.Format(" SELECT * FROM  {0} ", DBTableName);
                if (Filters.Count == 1)
                {
                    q += string.Format(" WHERE {0}={1} ", Filters.First<FilterParam>().FilterName, Filters.First<FilterParam>().FilterValue);
                }
                else if (Filters.Count > 1)
                {
                    q += " WHERE ";
                    for (int i = 0; i < Filters.Count - 1; i++)
                    {
                        FilterParam filter = Filters[i];
                        q += string.Format(" {0}={1} and ", filter.FilterName, filter.FilterValue);
                    }
                    q += string.Format(" {0}={1} ", Filters.Last<FilterParam>().FilterName, Filters.Last<FilterParam>().FilterValue);
                }

                if (Sorts.Count == 1)
                {
                    if (Sorts.First<SortParam>().SortType == SortTypes.ASC)
                        q += string.Format(" ORDER BY {0} ASC ", Sorts.First<SortParam>().FieldName);
                    else
                        q += string.Format(" ORDER BY {0} DESC ", Sorts.First<SortParam>().FieldName);
                }
                else if (Sorts.Count > 1)
                {
                    q += " ORDER BY ASC ";
                    for (int i = 0; i < Sorts.Count - 1; i++)
                    {
                        var sort = Sorts[i];
                        if (sort.SortType == SortTypes.ASC)
                            q += string.Format(" {0} ASC ,", sort.FieldName);
                        else
                            q += string.Format(" {0} DESC ,", sort.FieldName);
                    }
                    if (Sorts.Last<SortParam>().SortType == SortTypes.ASC)
                        q += string.Format(" {0} ASC", Sorts.Last<SortParam>().FieldName);
                    else
                        q += string.Format(" {0} DESC", Sorts.Last<SortParam>().FieldName);
                }
                // todo : add filters and order by on several fields
                q += string.Format(" LIMIT {0},{1}", Index.ToString(), Limit.ToString());
                return q;
            }
        }
    }
    public enum SortTypes
    {
        ASC = 1,
        DESC = 2
    }
}
