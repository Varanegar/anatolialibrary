using Aantoli.Framework.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.DataAdapter
{
    public abstract class BaseDataAdapter<DataListTemplate, Data>
        where DataListTemplate : BaseListEntity<Data>, new()
        where Data : BaseEntity, new()
    {
        public DataListTemplate GetAll(params object[] queryParams)
        {
            return GetAll(new DataListTemplate(), queryParams);
        }

        public DataListTemplate GetAll(DataListTemplate dataList, params object[] queryParams)
        {
            return new DataListTemplate();
        }

        public Data GetById(int id)
        {
            return new Data();
        }

        public bool IsDataIDValid(int ID)
        {
            return false;

        }

    }
}
