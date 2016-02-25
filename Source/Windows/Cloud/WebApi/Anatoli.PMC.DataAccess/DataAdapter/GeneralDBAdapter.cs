using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.ViewModels;
using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class GeneralDBAdapter<T> : BaseAdapter
        where T : BaseViewModel, new()
    {
        private static GeneralDBAdapter<T> instance = null;
        public static GeneralDBAdapter<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GeneralDBAdapter<T>();
                }
                return instance;
            }
        }
        GeneralDBAdapter() { }

        public List<T> GetAllData(DateTime lastUpload)
        {
            List<T> dataList = new List<T>();
            using (var context = new DataContext())
            {
                var data = context.All<T>(DBQuery.Instance.GetDBQuery(typeof(T)));
                dataList = data.ToList();
            }
            return dataList;
        }

    }
}
