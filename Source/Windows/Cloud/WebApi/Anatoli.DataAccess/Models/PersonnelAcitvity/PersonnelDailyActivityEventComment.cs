using Anatoli.DataAccess.Models.Route;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models.PersonnelAcitvity
{
    public class PersonnelDailyActivityEventComment : AnatoliBaseModel
    {
        [ForeignKey("PersonnelDailyActivityEvent")]
        public Guid PersonnelDailyActivityEventId { get; set; }
        public virtual PersonnelDailyActivityEvent PersonnelDailyActivityEvent { set; get; }
        [ForeignKey("PersonnelDailyActivityCommentType")]
        public Guid PersonnelDailyActivityCommentTypeId { get; set; }
        public virtual PersonnelDailyActivityCommentType PersonnelDailyActivityCommentType { set; get; }
        public bool IsDefault { get; set; }
        public string Description { get; set; }
    }
}
