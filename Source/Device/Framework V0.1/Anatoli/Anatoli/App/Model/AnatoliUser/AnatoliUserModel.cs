using Anatoli.Anatoliclient;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.AnatoliUser
{
    public class AnatoliUserModel : BaseDataModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel { get; set; }
        protected override string GetDataTable()
        {
            return null;
        }
    }
}
