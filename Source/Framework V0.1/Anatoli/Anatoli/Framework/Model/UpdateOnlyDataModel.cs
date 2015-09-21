using AnatoliLibrary.Anatoliclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.Model
{
    public abstract class UpdateOnlyDataModel : BaseDataModel
    {
        public abstract void CloudUpdateAsync();
        public abstract void LocalUpdateAsync();
    }
}
