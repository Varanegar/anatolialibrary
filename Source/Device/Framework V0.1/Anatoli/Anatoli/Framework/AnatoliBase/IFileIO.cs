using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    public interface IFileIO
    {
        string GetDataLoction();
        bool WriteAllText(string content, string path, string fileName);
        bool WriteAllBytes(byte[] content, string path, string fileName);
        string ReadAllText(string path, string fileName);
        byte[] ReadAllBytes(string path, string fileName);
    }
}
