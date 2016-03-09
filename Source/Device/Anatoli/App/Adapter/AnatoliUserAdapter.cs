using Anatoli.App.Model.AnatoliUser;
using Anatoli.Framework.DataAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Adapter
{
    public class AnatoliUserAdapter : DataAdapter<AnatoliUserListModel, AnatoliUserModel>
    {
       


        public override void CloudSave()
        {
            throw new NotImplementedException();
        }

        public override void LocalSave()
        {
            throw new NotImplementedException();
        }
    }
}
