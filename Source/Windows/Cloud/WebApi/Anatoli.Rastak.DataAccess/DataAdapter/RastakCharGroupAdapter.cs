using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using Anatoli.Rastak.DataAccess.Helpers;

namespace Anatoli.Rastak.DataAccess.DataAdapter
{
    public class RastakCharGroupAdapter : RastakBaseAdapter
    {
        private static RastakCharGroupAdapter instance = null;
        public static RastakCharGroupAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RastakCharGroupAdapter();
                }
                return instance;
            }
        }
        RastakCharGroupAdapter() { }
        public List<CharTypeViewModel> GetAllCharTypes(DateTime lastUpload)
        {
            List<CharTypeViewModel> charTypes = new List<CharTypeViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<CharTypeViewModel>(RastakDBQuery.Instance.GetCharType());
                data.ToList().ForEach(item =>
                {
                    var detail = context.All<CharValueViewModel>(RastakDBQuery.Instance.GetCharValue(item.ID));
                    item.CharValues = detail.ToList();
                });
                charTypes = data.ToList();
            }
            return charTypes;
        }
        public List<CharGroupViewModel> GetAllCharGroups(DateTime lastUpload)
        {
            List<CharGroupViewModel> charGroups = new List<CharGroupViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<CharGroupViewModel>(RastakDBQuery.Instance.GetCharGroup());
                data.ToList().ForEach(item =>
                {
                    var detail = context.All<CharTypeViewModel>(RastakDBQuery.Instance.GetCharGroupCharType(item.ID));
                    item.CharTypes = detail.ToList();
                });
                charGroups = data.ToList();
            }
            return charGroups;
        }

    }
}
