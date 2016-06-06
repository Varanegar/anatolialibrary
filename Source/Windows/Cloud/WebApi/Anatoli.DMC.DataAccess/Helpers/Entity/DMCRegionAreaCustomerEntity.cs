

namespace Anatoli.DMC.DataAccess.Helpers.Entity
{
    public class DMCRegionAreaCustomerEntity
    {
        public const string TabelName = "[VisitTemplateCustomer]";

        public const string RemoveByAreaId = "DELETE FROM " + TabelName + " WHERE [RegionAreaUniqueId] = '{0}'";

        public const string Insert = "INSERT INTO " + TabelName +
                                     "([UniqueId] " +
                                     ",[RegionAreaUniqueId]" +
                                     ",[CustomerUniqueId]" +
                                     ",OrderOf" +
                                     ",VisitTemplatePathId" +
                                     ",CustomerId" +
                                     ",VisitTemplateCustomerId )" +
                                     "VALUES(NEWID(), '{0}' , '{1}', " +
                                     "0," +
                                     "ISNULL((SELECT VTP.[VisitTemplatePathId] FROM VisitTemplatePath VTP WHERE VTP.UniqueId = '{0}' ),0)," +
                                     "ISNULL((SELECT C.[CustomerId] FROM Customer C WHERE C.UniqueId = '{1}' ),0)," +
                                     "ISNULL((SELECT MAX(T.[VisitTemplateCustomerId]) FROM " + TabelName + " T ),0)+1 );";
    }
}
