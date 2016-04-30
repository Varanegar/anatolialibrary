using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Service
{
    public class DataSourceResult
    {

        public IEnumerable Data { get; set; }

        public object Errors { get; set; }

        public int? Page { get; set; }

        public int? Total { get; set; }

        //public DataSourceResult(PagingList<T> lst)
        //{
        //    this.Data = lst.AsEnumerable();
        //    this.Page = lst.PageNumber;
        //    this.Total = lst.Total;
        //    this.Errors = lst.Errors;
        //} 
    }
}
