namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class ProductPicture : BaseModel
    {
        public byte[] Picture { get; set; }
        public int PictureTypeValueId { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
