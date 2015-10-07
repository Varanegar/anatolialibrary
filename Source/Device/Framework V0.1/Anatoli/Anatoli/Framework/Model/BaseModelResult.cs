using Anatoli.Anatoliclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.Model
{
    class BaseModelResult<Data> where Data : BaseDataModel
    {
        public AnatoliMetaInfo metaInfo { get; set; }
        public Data data { get; set; }
    }
}
