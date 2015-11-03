using Anatoli.App.Model.Store;
using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Manager
{
    public class MessageManager : BaseManager<BaseDataAdapter<MessageModel>, MessageModel>
    {

        public static async Task<bool> DeleteAsync(int id)
        {
            DeleteCommand command = new DeleteCommand("messages", new SearchFilterParam("msg_id", id.ToString()));
            var result = await LocalUpdateAsync(command);
            return (result > 0) ? true : false;
        }

        public static async void SetViewFlag(int id)
        {
            UpdateCommand command = new UpdateCommand("messages", new SearchFilterParam("msg_id", id.ToString()), new BasicParam("new_flag", "0"));
            var result = await LocalUpdateAsync(command);
        }
    }
}
