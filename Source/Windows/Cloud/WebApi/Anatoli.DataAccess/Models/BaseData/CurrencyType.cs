namespace Anatoli.DataAccess.Models
{
    using Common.DataAccess.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CurrencyType : BaseModel
    {
        //public int CurrencyType.Id { get; set; }
        [StringLength(100)]
        public string CurrencyTypeName { get; set; }
        public bool IsDefault { get; set; }
    }
}
