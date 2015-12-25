using Anatoli.Framework.AnatoliBase;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string RoleName { get { return "User"; } set{RoleName = value;} }
        public string PrivateOwnerId { get { return "CB11335F-6D14-49C9-9798-AD61D02EDBE1"; } set{PrivateOwnerId = value;}}
    }
}
