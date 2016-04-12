using System;

namespace Anatoli.ViewModels.AuthorizationModels
{
    public class PermissionCatalogViewModel : BaseViewModel
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Parent
        {
            get;
            set;
        }
    }
}