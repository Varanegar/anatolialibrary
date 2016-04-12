using System;

namespace Anatoli.ViewModels.AuthorizationModels
{
    public class PrincipalPermissionViewModel : BaseViewModel
    {
        public string PrincipalId
        {
            get;
            set;
        }

        public string Resource
        {
            get;
            set;
        }

        public string Action
        {
            get;
            set;
        }

        public bool Grant
        {
            get;
            set;
        }

        public Guid PermissionId
        {
            get;
            set;
        }
    }
}