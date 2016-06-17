namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class ProductPicture : AnatoliBaseModel
    {
        public Guid PictureTypeValueGuid { get; set; }
        public bool IsDefault { get; set; }
    
        public virtual Product Product { get; set; }
        public string ProductPictureName { get; set; }
    }
}
