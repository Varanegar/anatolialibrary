using System;
using System.Collections.Generic;
using Anatoli.DMC.DataAccess.DataAdapter;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.ViewModels.CommonModels;

namespace Anatoli.DMC.Business.Domain
{
    public class DMCVisitTemplatePathDomain //: DMCBusinessDomain<DMCCustomerViewModel, CustomerViewModel>, IDMCBusinessDomain<DMCCustomerViewModel, CustomerViewModel>
    {

        #region Ctors
        public DMCVisitTemplatePathDomain()
        { }
        #endregion

        #region Methods
        public Guid? GetParentIdById(Guid? id)
        {
            return DMCVisitTemplatePathAdapter.Instance.GetParentIdById(id);
        }


        public IList<DMCVisitTemplatePathViewModel> LoadArea1() // level 1
        {
            return DMCVisitTemplatePathAdapter.Instance.LoadArea1();
        }


        public List<DMCVisitTemplatePathViewModel> LoadAreaByParentId(Guid? id)
        {
            return DMCVisitTemplatePathAdapter.Instance.LoadAreaByParentId(id);
        }


        public DMCVisitTemplatePathViewModel GetViewById(Guid? id)
        {
            return DMCVisitTemplatePathAdapter.Instance.GetViewById(id);
        }

        public List<DMCVisitTemplatePathViewModel> GetAreaPathById(Guid? id)
        {
            return DMCVisitTemplatePathAdapter.Instance.GetAreaPathById(id);
        }
        public bool HasChild(Guid guid)
        {
            return DMCVisitTemplatePathAdapter.Instance.HasChild(guid);
        }

        public List<SelectListItemViewModel> LoadByLevel(int level, Guid? areaId)
        {
            return DMCVisitTemplatePathAdapter.Instance.LoadByLevel(level, areaId);
        }
        #endregion


    }
}
