using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.AppModels
{
    public class DataOwnerViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string WebHookURI { get; set; }

        public string WebHookUsername { get; set; }

        public string WebHookPassword { get; set; }

        public Guid AnatoliContactId { get; set; }

        public string AnatoliContactName { get; set; }

        public Guid ApplicationOwnerId { get; set; }

        public string ApplicationOwnerName { get; set; }
    }
}
