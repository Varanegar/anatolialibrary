using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WcfAppServer.Common.Enums;
using WcfAppServer.Common.Interfaces;
using VSPD;
using System.Data;

namespace WcfAppServer.ClientProxy.ClientProxies
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
            
            if (ProxyFeatureUsed(aConnectionString))
            {
                VSPDRunProxy.VNApplicationServer = GetProxyServer(aConnectionString);
                return VSPDRunProxy.Instance.ExecuteScalarFromDB(SQLCommand, GetHQConnectionString2(aConnectionString));

            }
            else
            {

                return Channel.ExecuteScalarFromDB(SQLCommand, aConnectionString);

            }

          //  return Channel.ExecuteScalarFromDB("select count(*) from customer",
            //        @"Password=totalcommander;Persist Security Info=True;User ID=PMC;Initial Catalog=S_CN;Data Source=.\db;");
        }


        public bool ProxyFeatureUsed(string aConnectionString)
        {
            

            if (aConnectionString.IndexOf(ProxyKey) != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }

        public string ProxyKey = "ProxyServer:";
        public string GetHQConnectionString2(string aConnectionString)
        {
            string ProxyServer = "";
            try
            {
                int StartIndex = aConnectionString.IndexOf(ProxyKey);
                int EndIndex = aConnectionString.IndexOf(";", StartIndex);
                ProxyServer = aConnectionString.Substring(StartIndex, (EndIndex - StartIndex + 1));
                aConnectionString = aConnectionString.Replace(ProxyServer, "");
                return aConnectionString;
            }
            catch (System.Exception e2)
            {
                throw new System.Exception("GetHQConnectionString2: " + e2.Message + " aConnectionString= " + aConnectionString + " ProxyKey " + ProxyKey + " ProxyServer: " + ProxyServer);
          
                return aConnectionString;
            }
        }

        public string GetProxyServer(string aConnectionString)
        {
            try
            {
                int StartIndex = aConnectionString.IndexOf(ProxyKey);
                int EndIndex = aConnectionString.IndexOf(";", StartIndex);
                string ProxyServer = aConnectionString.Substring(StartIndex, (EndIndex - StartIndex + 1));
                return ProxyServer;
            }
            catch(System.Exception e2)
            {
                throw new System.Exception("GetProxyServer: " + e2.Message + " aConnectionString= " + aConnectionString + " ProxyKey " + ProxyKey);
                return null;
            }
        }

        public string ExecuteToDB(string Query, string aConnectionString)
        {

            if (ProxyFeatureUsed(aConnectionString))
            {
                VSPDRunProxy.VNApplicationServer = GetProxyServer(aConnectionString);
                return  VSPDRunProxy.Instance.ExecuteToDB(Query, GetHQConnectionString2(aConnectionString));
            }
            else
            {
                Channel.ExecuteToDB(Query, aConnectionString);
            }

                return "";
        }

        public DataSet ExecuteReaderFromDB(string Aqery, string aConnectionString)
        {

            if (ProxyFeatureUsed(aConnectionString))
            {
                VSPDRunProxy.VNApplicationServer = GetProxyServer(aConnectionString);
                return VSPDRunProxy.Instance.ExecuteReaderFromDB(Aqery, GetHQConnectionString2(aConnectionString));
            }
            else
            {
                return Channel.ExecuteReaderFromDB(Aqery, aConnectionString);

            }
                    

            //return Channel.ExecuteReaderFromDB("select top 10 * from customer",
            //        @"Password=totalcommander;Persist Security Info=True;User ID=PMC;Initial Catalog=S_CN;Data Source=.\db;");
        }

    }
}