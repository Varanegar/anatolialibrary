using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.products
{
    public class ProductModel
    {
        List<ProductField> _fields;

        public List<ProductField> Fields
        {
            get { return _fields; }
        }
        DateTime _additionDate;
        public DateTime AdditionDate
        {
            get { return _additionDate; }
        }
        public int _rateCount;
        public int RateCount
        {
            get { return _rateCount; }
        }
        public async void Rate()
        {
            _rateCount = await SaveRateAsync();
        }
        async Task<int> SaveRateAsync()
        {
            throw new NotImplementedException();
        }
        public ProductName Name { get; set; }
        public class ProductName
        {
            public string Name { get; set; }
        }
    }
}
