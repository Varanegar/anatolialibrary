using AnatoliaLibrary.anatoliaclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.user
{
    public class ShippingInfo : SyncDataModel
    {
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ShippingInfo(AnatoliaClient client) : base(client) { }

        public override void LocalSaveAsync()
        {
            throw new NotImplementedException();
        }

        public override void CloudSaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
