using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Authorise
{
    public sealed class CrowdSSOAPICall
    {

        private readonly String name;
        private readonly int value;

        public static readonly CrowdSSOAPICall Authenticate = new CrowdSSOAPICall(1, "authentication?username=");
        public static readonly CrowdSSOAPICall ChangePassword = new CrowdSSOAPICall(2, "user/password?username=");
        public static readonly CrowdSSOAPICall RequestPasswordReset = new CrowdSSOAPICall(3, "user/mail/password?username=");
        public static readonly CrowdSSOAPICall UsersInGroup = new CrowdSSOAPICall(4, "group/user/nested?groupname=");
        public static readonly CrowdSSOAPICall UserDetail = new CrowdSSOAPICall(5, "user?username=");
        public static readonly CrowdSSOAPICall UserAttribute = new CrowdSSOAPICall(6, "user/attribute?username=");

        private CrowdSSOAPICall(int value, String name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return name;
        }

    }

}
