using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliLibrary.Anatoliclient
{
    public interface IFileIO
    {
        bool WriteAll();
        string ReadAll();
    }
}
