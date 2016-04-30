using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Service
{
    public class PagingList<T> : List<T>
    {
        public PagingList()
        {
        }

        public PagingList(IList<T> data, int? pageNamber, int? total)
        {
            this.AddRange(data);
            this.PageNumber = pageNamber;
            this.Total = total;
        }

        //IList<T> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? Total { get; set; }
        public object Errors { get; set; }


    }
}
