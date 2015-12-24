using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.ProductModels
{
    public class ProductPictureViewModel : BaseViewModel
    {
        public string ProductPictureName { get; set; }
        public bool IsDefault { get; set; }
    }
}
