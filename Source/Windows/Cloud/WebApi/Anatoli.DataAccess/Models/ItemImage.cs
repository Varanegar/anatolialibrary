namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic; 
       
    public class ItemImage : BaseModel
    {
        public string TokenId { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
    }
}
