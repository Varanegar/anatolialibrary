using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    public interface IFileIO
    {
        bool WriteAll();
        string ReadAll();
    }
}
