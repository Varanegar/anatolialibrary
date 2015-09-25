using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WcfAppServer.Common.Enums;
using WcfAppServer.Common.Interfaces;
using VSPD;
using System.Data;

namespace WcfAppServer.ConsoleClient.ClientProxies
{
    public class VSPDClient : ClientBase<IVSPDBCls>, IVSPDBCls
    {
        public VSPDClient()
        {
            
        }

        public VSPDClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }


        public string ExecuteScalarFromDB(string SQLCommand, string aConnectionString)
        {
            return Channel.ExecuteScalarFromDB("select count(*) from customer",
                    @"Password=totalcommander;Persist Security Info=True;User ID=PMC;Initial Catalog=S_CN;Data Source=.\db;");
        }

        public string ExecuteToDB(string Query, string aConnectionString)
        {
                Channel.ExecuteToDB("select count(*) from customer",
                       @"Password=totalcommander;Persist Security Info=True;User ID=PMC;Initial Catalog=S_CN;Data Source=.\db;");
                return "";
        }

        public DataSet ExecuteReaderFromDB(string Aqery, string aConnectionString)
        {
            throw new System.Exception("ExecuteReaderFromDB2 : " + aConnectionString);
            return Channel.ExecuteReaderFromDB("select top 10 * from customer",
                    @"Password=totalcommander;Persist Security Info=True;User ID=PMC;Initial Catalog=S_CN;Data Source=.\db;");
        }

    }
}