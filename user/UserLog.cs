using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.user
{
    public class UserLog
    {
        IFileIO _file;
        public UserLog(IFileIO file)
        {
            _file = file;
        }
        public async Task LogActivityAsync()
        {
            throw new NotImplementedException();
        }
        public async Task SendLogAsync()
        {
            throw new NotImplementedException();
        }
    }
}
