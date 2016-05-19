using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DMC.DataAccess.Helpers.Entity
{
    public class DMCRegionAreaPointEntity
    {
        public const string TabelName = "[GisRegionAreaPoint]";

        public const string RemoveByAreaId = "DELETE FROM " + TabelName + " WHERE [RegionAreaUniqueId] = '{0}'";

        public const string Insert = "INSERT INTO " + TabelName +
                                     "([UniqueId]" +
                                     ",[RegionAreaUniqueId]" +
                                     ",[CustomerUniqueId]" +
                                     ",[Priority]" +
                                     ",[Latitude]" +
                                     ",[Longitude]" +
                                     ",[IntId])" +
                                     "VALUES(NEWID(), '{0}' , {1}, {2}, {3}, {4}, {5});";


    }
}
