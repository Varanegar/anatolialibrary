using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using Anatoli.PMC.DataAccess.Helpers;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class CharGroupAdapter : BaseAdapter
    {
        private static CharGroupAdapter instance = null;
        public static CharGroupAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CharGroupAdapter();
                }
                return instance;
            }
        }
        CharGroupAdapter() { }
        public List<CharTypeViewModel> GetAllCharTypes(DateTime lastUpload)
        {
            List<CharTypeViewModel> charTypes = new List<CharTypeViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<CharTypeViewModel>(DBQuery.Instance.GetCharType());
                data.ToList().ForEach(item =>
                {
                    var detail = context.All<CharValueViewModel>(DBQuery.Instance.GetCharValue(item.ID));
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
                var data = context.All<CharGroupViewModel>(DBQuery.Instance.GetCharGroup());
                data.ToList().ForEach(item =>
                {
                    var detail = context.All<CharTypeViewModel>(DBQuery.Instance.GetCharGroupCharType(item.ID));
                    item.CharTypes = detail.ToList();
                });
                charGroups = data.ToList();
            }
            return charGroups;
        }

    }
}
