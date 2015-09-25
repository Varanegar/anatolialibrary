using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.ServiceModel;
using WcfAppServer.ClientProxy;

namespace VSPD
{
    public class VSPDBCls : IVSPDBCls
    {
        public string ProxyKey = "ProxyServer:";

        public string GetProxyServer(string aConnectionString)
        {
            try
            {
                int StartIndex = aConnectionString.IndexOf(ProxyKey);
                int EndIndex = aConnectionString.IndexOf(";", StartIndex);
                string ProxyServer = aConnectionString.Substring(StartIndex, (EndIndex - StartIndex + 1));
                return ProxyServer.Replace(ProxyKey,"").Replace(";","");
            }
            catch (System.Exception e2)
            {
                throw new System.Exception("GetProxyServer: " + e2.Message + " aConnectionString= " + aConnectionString + " ProxyKey " + ProxyKey);
                return null;
            }
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

        public DataSet ExecuteReaderFromDB(string SQLCommand, string aConnectionString)
        {
            try
            {
                if (ProxyFeatureUsed(aConnectionString))
                {
                    VSPDRunProxy.VNApplicationServer = GetProxyServer(aConnectionString);
                    return VSPDRunProxy.Instance.ExecuteReaderFromDB(SQLCommand, GetHQConnectionString2(aConnectionString));
                }
                else
                {

                    DataTable DT = new DataTable();
                    SqlDataAdapter SDA = new SqlDataAdapter(SQLCommand, aConnectionString);
                    SDA.SelectCommand.CommandTimeout = 0;
                    SDA.Fill(DT);

                    DataSet aDataSet = new DataSet();
                    aDataSet.Tables.Add(DT);
                    return aDataSet;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                return null;
            }

        }

        public string ExecuteScalarFromDB(string SQLCommand, string aConnectionString)
        {
            try
            {
                if (ProxyFeatureUsed(aConnectionString))
                {
                    VSPDRunProxy.VNApplicationServer = GetProxyServer(aConnectionString);
                    return VSPDRunProxy.Instance.ExecuteScalarFromDB(SQLCommand, GetHQConnectionString2(aConnectionString));

                }
                else
                {
                    SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
                    aSqlConnection.Open();
                    SqlCommand command = new SqlCommand(SQLCommand, aSqlConnection);
                    return command.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                return null;
            }


        }

        public string ExecuteToDB(string Query, string aConnectionString)
        {
            try
            {
                if (ProxyFeatureUsed(aConnectionString))
                {
                    VSPDRunProxy.VNApplicationServer = GetProxyServer(aConnectionString);
                    return VSPDRunProxy.Instance.ExecuteToDB(Query, GetHQConnectionString2(aConnectionString));
                }
                else
                {
                    SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
                    aSqlConnection.Open();
                    SqlCommand command = new SqlCommand(Query, aSqlConnection);
                    command.ExecuteNonQuery();
                    return "";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                return e.Message;
            }

        }
    }
}
