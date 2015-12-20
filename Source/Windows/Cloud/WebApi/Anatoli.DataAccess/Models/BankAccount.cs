namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic; 
       
    public class BankAccount : BaseModel
    {
        public string BankAccountNo { get; set; }
        public string BankAccountName { get; set; }
    }
}
