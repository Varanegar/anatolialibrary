namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic; 
       
    public class BankAccount : BaseModel
    {
        //public int BankAccountId { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountName { get; set; }
    }
}
