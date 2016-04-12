using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels
{
    public class BaseViewModel
    {
        public int ID { get; set; }
        public Guid UniqueId { get; set; }
        public Guid ApplicationOwnerId { get; set; }
        public Guid DataOwnerId { get; set; }
        public Guid DataCenterOwnerId { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
