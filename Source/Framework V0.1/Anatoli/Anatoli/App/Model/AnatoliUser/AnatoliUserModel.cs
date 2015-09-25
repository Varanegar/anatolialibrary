using Anatoli.Anatoliclient;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.AnatoliUser
{
    public class AnatoliUserModel : SyncDataModel
    {
        string _userId;
        public string UserId { get { return _userId; } }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel { get; set; }
        public override void LocalSaveAsync()
        {
            throw new NotImplementedException();
        }

        public override void CloudSaveAsync()
        {
            throw new NotImplementedException();
        }

        public override void CloudUpdateAsync()
        {
            throw new NotImplementedException();
        }

        public override void LocalUpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
