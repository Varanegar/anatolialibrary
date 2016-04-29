using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using System.Data;

namespace Anatoli.DMC.DataAccess.Helpers.Entity
{
    public class DMCGetIdEntity
    {
        public string TableName { get; set; }
        [OutputParameterAttribute(DbType.Int32, 0)]
        public int Id { get; set; }
    }
}
