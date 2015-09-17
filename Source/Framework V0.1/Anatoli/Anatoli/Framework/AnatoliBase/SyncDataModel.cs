using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliLibrary.Anatoliclient
{
    public abstract class SyncDataModel
    {
        protected AnatoliClient _client;
        protected AnatoliClient Client
        {
            get { return _client; }
        }
        public SyncDataModel(AnatoliClient client)
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
