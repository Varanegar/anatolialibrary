using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace VSPD
{
    [ServiceContract]
    public interface IVSPDBCls
    {
        [OperationContract]
        DataSet ExecuteReaderFromDB(string SQLCommand, string aConnectionString);

        [OperationContract]
        string ExecuteScalarFromDB(string SQLCommand, string aConnectionString);

        [OperationContract]
        string ExecuteToDB(string Query, string aConnectionString);

    }

  
}
