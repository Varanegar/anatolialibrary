using System;
using System.Collections.Generic;
using Anatoli.DMC.DataAccess.DataAdapter;
using Anatoli.DMC.ViewModels.Gis;
using Anatoli.ViewModels.CommonModels;

namespace Anatoli.DMC.Business.Domain
{
    public class DMCCompanyPersonelDomain
    {
        #region Methods
        public List<SelectListItemViewModel> LoadPersonByGroup(Guid? groupId)
        {
            return DMCCompanyPersonelAdapter.Instance.LoadPersonByGroup(groupId);
        }
        public List<SelectListItemViewModel> LoadGroupByArea(Guid? areaId)
        {
            return DMCCompanyPersonelAdapter.Instance.LoadGroupByArea(areaId);
        }
 
        internal List<DMCPointViewModel> LoadPersonelsProgramPath(string date, List<Guid> personIds)
        {
            return DMCCompanyPersonelAdapter.Instance.LoadPersonelsProgramPath(date, personIds);
        }
        #endregion
        /*
 * public List<DMCPointViewModel> LoadPersonelsPath(string date, List<Guid> personIds)
        {
            return DMCCompanyPersonelAdapter.Instance.LoadPersonsPath(date, personIds);
        }

        public List<DMCPointViewModel> LoadPersonelsAtivities(string date, List<Guid> ids,
            bool order, bool lackOrder, bool lackVisit, bool stopWithoutActivity, bool stopWithoutCustomer)
        {
            return DMCCompanyPersonelAdapter.Instance.LoadPersonelsAtivities(date, ids,
                        order, lackOrder, lackVisit, stopWithoutActivity,  stopWithoutCustomer);
        }
 * */
    }
}
