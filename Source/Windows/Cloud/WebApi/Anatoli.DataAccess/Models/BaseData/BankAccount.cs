namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations; 
       
    public class BankAccount : AnatoliBaseModel
    {
        [StringLength(50)]
        public string BankAccountNo { get; set; }
        [StringLength(200)]
        public string BankAccountName { get; set; }
    }
}
