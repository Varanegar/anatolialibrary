namespace Anatoli.DataAccess.Models
{
    using Anatoli.DataAccess.Models.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PrincipalStock : AnatoliBaseModel
    {
        public Guid PrincipalId { get; set; }
        [ForeignKey("Stock")]
        public Guid StockId { get; set; }
        public virtual Stock Stock { get; set; }

    }
}
    