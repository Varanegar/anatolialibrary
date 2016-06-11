using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.DMC.DataAccess.DataAdapter;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.DMC.ViewModels.Gis;

namespace Anatoli.DMC.Business.Domain
{
    public class DMCRegionAreaCustomerDomain
    {
        #region Ctors
        public DMCRegionAreaCustomerDomain()
        { }
        #endregion
        
        #region Methods
        public List<DMCRegionAreaCustomerViewModel> LoadCustomerViewByAreaId(Guid? areaid, bool selected)
        {
            return DMCRegionAreaCustomerAdapter.Instance.LoadCustomerViewByAreaId(areaid, selected);

        }

        public List<DMCPointViewModel> LoadCustomerPointByAreaId(Guid? areaid, Guid? routeid = null,
                bool showcustrout = false,
                bool showcustotherrout = false,
                bool showcustwithoutrout = false)
        {
            return DMCRegionAreaCustomerAdapter.Instance.LoadCustomerPointByAreaId(areaid, routeid,
                showcustrout,
                showcustotherrout,
                showcustwithoutrout );

        }

        public bool AddCustomerToRegionArea(Guid customerId, Guid areaId)
        {
            return DMCRegionAreaCustomerAdapter.Instance.AddCustomerToRegionArea(customerId, areaId);
        }

        public bool RemoveCustomerFromRegionArea(Guid customerId, Guid areaId)
        {
            return DMCRegionAreaCustomerAdapter.Instance.RemoveCustomerFromRegionArea(customerId, areaId);
        }

        public List<DMCRegionAreaCustomerViewModel> LoadCustomerWithouteLocation(Guid areaId)
        {
            return DMCRegionAreaCustomerAdapter.Instance.LoadCustomerWithoutLocation(areaId);
        }
        public List<DMCRegionAreaCustomerViewModel> LoadCustomerInvalidLocation(Guid areaId)
        {
            return DMCRegionAreaCustomerAdapter.Instance.LoadCustomerInvalidLocation(areaId);
        }
        public List<DMCRegionAreaCustomerViewModel> LoadCustomerValidLocation(Guid areaId)
        {
            return DMCRegionAreaCustomerAdapter.Instance.LoadCustomerValidLocation(areaId);
        }
        public int GetCustomerWithoutLocationCount(Guid areaId)
        {
            return DMCRegionAreaCustomerAdapter.Instance.GetCustomerWithoutLocationCount(areaId);
        }
        public int GetCustomerInvalidLocationCount(Guid areaId)
        {
            return DMCRegionAreaCustomerAdapter.Instance.GetCustomerInvalidLocationCount(areaId);
        }
        #endregion



    }
}
