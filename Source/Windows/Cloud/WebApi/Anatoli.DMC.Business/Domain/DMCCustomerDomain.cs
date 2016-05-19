using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.DMC.DataAccess.DataAdapter;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.DMC.ViewModels.Base;

namespace Anatoli.DMC.Business.Domain
{
    public class DMCCustomerDomain
    {
        #region Ctors
        public DMCCustomerDomain()
        { }
        #endregion
        
        #region Methods
        public List<DMCCustomerViewModel> LoadCustomerBySearchTerm(string searchStr)
        {
            return DMCCustomerAdapter.Instance.LoadCustomerBySearchTerm(searchStr);
        }

        public bool UpdateCustomerLatLng(DMCCustomerPointViewModel customerPoint)
        {
            return DMCCustomerAdapter.Instance.UpdateCustomerLatLng(customerPoint);
        }
        
        #endregion


    }
}
