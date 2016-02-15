using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework
{
    public class AnatoliHandledException : Exception
    {
        public AnatoliHandledException(Exception innerexception)
            : base("This exception has been handeled", innerexception)
        {

        }
    }
}
