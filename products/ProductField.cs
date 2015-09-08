using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.products
{
    public class ProductField<T>
    {
        public string Name;
        public T Value;
    }
    public class ProductField : ProductField<Price>
    {

    }
}
