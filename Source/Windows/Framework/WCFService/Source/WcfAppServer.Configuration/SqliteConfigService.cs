using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WcfAppServer.Common.DTOs;
using WcfAppServer.Common.Interfaces;
using WcfAppServer.Logging;

namespace WcfAppServer.Configuration
{
    [ServiceContract]
    public class SQLiteConfigService : AbstractConfigService, IConfigService
    {
        private const string AdminSql =
            "select c.Address, c.IncludeMex, c.IncludeExceptionDetailInFault from WcfService s join WcfServiceConfig c on s.Id = c.WcfServiceId where s.IsAdmin=1";

        private const string ServicesSql =
            "select l.id, l.filepath, s.ServiceId, s.ServiceAssemblyName, s.ServiceClassName, s.ContractAssemblyName, s.ContractClassName, c.Address, c.IncludeMex, c.IncludeExceptionDetailInFault from WcfServiceLibrary l join WcfService s on l.Id = s.WcfServiceLibraryId join WcfServiceConfig c on s.Id = c.WcfServiceId where s.IsAdmin=0";

        private static string ConnectionString
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SQLiteDb"].ConnectionString;
                return connectionString;
            }
        }

        #region IConfigService Members

        public SQLiteConfigService()
        {
            try
            {
                if (ConnectionString == null)
                {
                    // accessing the connectionString will result in an exception 
                    // if the config cannot be found.
                }
            }
            catch
            {
                throw new Exception("No connection string could be found for the SQLiteConfigService");
            }
        }

        public new List<IWcfServiceLibrary> GetWcfServiceLibraries()
        {
            var wcfServiceLibraries = new List<IWcfServiceLibrary>();
            IWcfServiceLibrary wcfServiceLibrary = null;
            string id = string.Empty;

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    using (var command = new SQLiteCommand(ServicesSql, connection) { CommandType = CommandType.Text })
                    {
                        connection.Open();

                        SQLiteDataReader rd = command.ExecuteReader();

                        while (rd.Read())
                        {
                            if (id != rd[0].ToString())
                            {
                                id = rd[0].ToString();
                                string filePath = rd[1].ToString();
                                wcfServiceLibrary = WcfServiceLibrary.Create(filePath);

                                wcfServiceLibraries.Add(wcfServiceLibrary);                                
                            }

                            IWcfService wcfService = WcfService.Create(
                                rd[2].ToString(),
                                rd[3].ToString(),
                                rd[4].ToString(),
                                rd[5].ToString(),
                                rd[6].ToString());

                            string address = rd[7].ToString();
                            Binding binding = WcfBindingHelper.GetInferredBinding(address, false);

                            IWcfServiceConfig wcfServiceConfig = WcfServiceConfig.Create(
                                address,
                                binding,
                                wcfService,
                                Convert.ToBoolean(rd[8]),
                                Convert.ToBoolean(rd[9]));

                            if (wcfServiceLibrary == null ||
                                wcfService == null ||
                                wcfServiceConfig == null)
                                throw new Exception("Problem reading from the configuration database");

                            wcfServiceLibrary.WcfServiceConfigs.Add(wcfServiceConfig);
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                LoggingService.Logger.Error(e);
                throw;
            }
            catch (Exception e)
            {
                LoggingService.Logger.Error(e);
                throw;
            }
            return wcfServiceLibraries;
        }

        public new IWcfServiceConfig GetAppServerAdmin()
        {
            IWcfServiceConfig wcfServiceConfig = null;
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    using (var command = new SQLiteCommand(AdminSql, connection) { CommandType = CommandType.Text })
                    {
                        connection.Open();

                        SQLiteDataReader rd = command.ExecuteReader();

                        while (rd.Read())
                        {
                            var address = rd[0].ToString();
                            Binding binding = WcfBindingHelper.GetInferredBinding(address, false);

                            wcfServiceConfig = WcfServiceConfig.Create(
                                address,
                                binding,
                                null,
                                Convert.ToBoolean(rd[1]),
                                Convert.ToBoolean(rd[2]));
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                LoggingService.Logger.Trace("");
                LoggingService.Logger.Trace("************************************************************");
                LoggingService.Logger.Error(
                    "PROBLEM READING FROM SQLite DATABASE.  Please read the INSTALLATION-README.txt for instructions on how to setup access to the SQLite database");
                LoggingService.Logger.Trace("************************************************************");
                LoggingService.Logger.Trace("");
                
                LoggingService.Logger.Error(e);
            }
            catch (Exception e)
            {
                LoggingService.Logger.Error(e);
            }

            return wcfServiceConfig;
        }

        #endregion
    }
}