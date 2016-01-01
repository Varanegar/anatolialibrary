using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.PMC.ViewModels
{
    public class PMCBaseViewModel
    {
        public bool HasAttachment { get; set; }
        public bool HasNote { get; set; }
        public bool ReadOnly { get; set; }
        public bool IsAdded { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }
        public bool IsSaveRequired { get; set; }
        public bool IsUnchanged { get; set; }
        public string ModifiedDate { get; set; }
        public int AppUserId { get; set; }
    }
}
