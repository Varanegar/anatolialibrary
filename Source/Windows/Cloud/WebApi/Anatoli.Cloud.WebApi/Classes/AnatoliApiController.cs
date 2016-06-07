using System;
using System.Web;
using Anatoli.Cloud.WebApi.Controllers;
using System.Globalization;
using Anatoli.DataAccess.Models;

namespace Anatoli.Cloud.WebApi.Classes
{
    public abstract class AnatoliApiController : BaseApiController
    {
        public bool GetRemovedData
        {
            get
            {
                string data = HttpContext.Current.Request.Headers["GetRemovedData"];
                return (data == null) ? true : bool.Parse(data);
            }
        }

        public Guid OwnerKey
        {
            get
            {
                return Guid.Parse(HttpContext.Current.Request.Headers["OwnerKey"]);
            }
        }
        public Guid DataOwnerKey
        {
            get
            {
                if (HttpContext.Current.Request.Headers["DataOwnerKey"] == null)
                    return OwnerKey;
                else
                    return Guid.Parse(HttpContext.Current.Request.Headers["DataOwnerKey"]);
            }
        }
        public Guid DataOwnerCenterKey
        {
            get
            {
                if (HttpContext.Current.Request.Headers["DataOwnerCenterKey"] == null)
                    return DataOwnerKey;
                else
                    return Guid.Parse(HttpContext.Current.Request.Headers["DataOwnerCenterKey"]);
            }
        }

        public OwnerInfo OwnerInfo
        {
            get
            {
                return new OwnerInfo
                {
                    ApplicationOwnerKey = OwnerKey,
                    DataOwnerCenterKey = DataOwnerCenterKey,
                    DataOwnerKey = DataOwnerKey
                };
            }
        }

        public DateTime GetDateFromString(string dateStr)
        {
            var validDate = DateTime.MinValue;
            try { validDate = DateTime.Parse(dateStr); }
            catch (Exception ex) {
                DateTime.TryParseExact(dateStr, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out validDate);
            }
            return validDate;
        }
    }
}