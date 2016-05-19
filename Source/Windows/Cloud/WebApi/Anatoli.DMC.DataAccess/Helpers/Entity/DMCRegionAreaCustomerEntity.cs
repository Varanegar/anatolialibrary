

namespace Anatoli.DMC.DataAccess.Helpers.Entity
{
    public class DMCRegionAreaCustomerEntity
    {
        public const string TabelName = "[GisRegionAreaCustomer]";

        public const string RemoveByAreaId = "DELETE FROM " + TabelName + " WHERE [RegionAreaUniqueId] = '{0}'";

        public const string Insert = "INSERT INTO " + TabelName +
                                     "([UniqueId] " +
                                     ",[RegionAreaUniqueId]" +
                                     ",[CustomerUniqueId]" +
                                     ",[IntId])" +
                                     "VALUES(NEWID(), '{0}' , '{1}', {2});";
    }
}
