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
    public class MessageManager : BaseManager<MessageModel>
    {

        public static async Task<bool> DeleteAsync(int id)
        {
            DeleteCommand command = new DeleteCommand("messages", new EqFilterParam("msg_id", id.ToString()));
            var result = await BaseDataAdapter<MessageModel>.UpdateItemAsync(command);
            return (result > 0) ? true : false;
        }

        public static async void SetViewFlag(List<int> ids)
        {
            string q = "UPDATE messages SET new_flag=1 WHERE ";

            foreach (var item in ids)
            {
                q += " msg_id=" + item.ToString() + " OR";
            }
            q += " 1=0";

            StringQuery command = new StringQuery(q);
            command.Unlimited = true;
            var result = await BaseDataAdapter<MessageModel>.UpdateItemAsync(command);
        }
    }
}
