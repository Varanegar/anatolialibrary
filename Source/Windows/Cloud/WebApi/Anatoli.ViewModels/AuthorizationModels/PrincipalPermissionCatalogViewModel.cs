using System;

namespace Anatoli.ViewModels.AuthorizationModels
{
    public class PrincipalPermissionCatalogViewModel : BaseViewModel
    {
        public Guid Id
        {
            get;
            set;
        }

        public string PrincipalId
        {
            get;
            set;
        }

        public bool Grant
        {
            get;
            set;
        }

        public Guid PermissionCatalogId
        {
            get;
            set;
        }
    }
}