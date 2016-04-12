using System;

namespace Anatoli.ViewModels.AuthorizationModels
{
    public class PermissionViewModel : BaseViewModel
    {
        public string Action
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Resource
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }
    }
}