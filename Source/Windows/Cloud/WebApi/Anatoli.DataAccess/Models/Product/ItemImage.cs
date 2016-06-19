namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations; 
       
    public class ItemImage : AnatoliBaseModel
    {
        [StringLength(50)]
        public string TokenId { get; set; }
        [StringLength(100)]
        public string ImageName { get; set; }
        [StringLength(50)]
        public string ImageType { get; set; }
        public bool IsDefault { get; set; }
    }
}
