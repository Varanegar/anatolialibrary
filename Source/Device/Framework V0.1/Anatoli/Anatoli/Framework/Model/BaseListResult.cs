using Anatoli.Framework.AnatoliBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.Model
{
    public class BaseListResult<Data> where Data : BaseDataModel
    {
        public AnatoliMetaInfo metaInfo { get; set; }
        public List<Data> items { get; set; }
    }
}
