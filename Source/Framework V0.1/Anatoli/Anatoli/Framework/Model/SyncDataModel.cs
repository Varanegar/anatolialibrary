using AnatoliLibrary.Anatoliclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.Model
{
    public abstract class SyncDataModel : UpdateOnlyDataModel
    {
        public abstract void LocalSaveAsync();
        public abstract void CloudSaveAsync();
    }
}
