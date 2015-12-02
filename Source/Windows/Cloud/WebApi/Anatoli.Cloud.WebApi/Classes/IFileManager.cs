using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Anatoli.Cloud.WebApi.Classes
{
    public interface IFileManager
    {
        Task Save(System.Web.HttpPostedFileBase file, ObjectTypes objectTypes, string token);
        Task Remove(string token, ObjectTypes objectTypes, string fileName);
        string GetPath(string token, ObjectTypes objectTypes, string fileName);
        List<string> GetFileNames(string token, ObjectTypes objectTypes);
    }
}
