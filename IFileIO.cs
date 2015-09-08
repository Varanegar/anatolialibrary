using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary
{
    public interface IFileIO
    {
        bool WriteAll();
        string ReadAll();
    }
}
