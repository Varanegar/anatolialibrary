namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CharGroup : BaseModel
    {
        public int CharGroupCode { get; set; }
        public string CharGroupName { get; set; }
        //public Guid CharGroupId { get; set; }
    }
}
