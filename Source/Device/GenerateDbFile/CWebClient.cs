using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Framework.AnatoliBase;

namespace GenerateDbFile
{
    class CWebClient : AnatoliWebClient
    {
        public override bool IsOnline()
        {
            return true;
        }
    }
}
