using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.anatoliaclient
{
    public abstract class SyncDataModel
    {
        protected AnatoliaClient _client;
        protected AnatoliaClient Client
        {
            get { return _client; }
        }
        public SyncDataModel(AnatoliaClient client)
        {
            _client = client;
        }
        public async Task SaveAsync()
        {
            await Task.Run(() => LocalSaveAsync());
            if (_client.WebClient.IsOnline())
            {
                await Task.Run(() => CloudSaveAsync());
            }
        }
        public abstract void LocalSaveAsync();
        public abstract void CloudSaveAsync();
    }
}
