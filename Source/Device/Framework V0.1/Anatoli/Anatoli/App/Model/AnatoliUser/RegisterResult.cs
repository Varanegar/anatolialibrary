using Anatoli.Anatoliclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anatoli.App.Model.AnatoliUser
{
    public class RegisterResult
    {
        public AnatoliMetaInfo metaInfo { get; set; }
        public AnatoliUserModel userModel { get; set; }
    }
}
