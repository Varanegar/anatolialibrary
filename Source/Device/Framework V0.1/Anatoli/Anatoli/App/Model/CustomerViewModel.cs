﻿using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model
{
    public class CustomerViewModel : BaseDataModel
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string NationalCode { get; set; }

        //public List<BasketViewModel> Baskets { get; set; }
    }
}
