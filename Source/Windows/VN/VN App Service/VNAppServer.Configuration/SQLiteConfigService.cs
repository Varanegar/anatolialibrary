using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.ServiceModel;
using System.ServiceModel.Channels;
using VNAppServer.Common.DTOs;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Configuration
{
    [ServiceContract]
    public class SQLiteConfigService : AbstractConfigService, IConfigService
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string AdminSql =
            "select c.Address, c.IncludeMex, c.IncludeExceptionDetailInFault from WcfService s join WcfServiceConfig c on s.Id = c.WcfServiceId where s.IsAdmin=1";

        private const string ServicesSql =
            "select l.id, l.filepath, s.ServiceId, s.ServiceAssemblyName, s.ServiceClassName, s.ContractAssemblyName, s.ContractClassName, c.Address, c.IncludeMex, c.IncludeExceptionDetailInFault from WcfServiceLibrary l join WcfService s on l.Id = s.WcfServiceLibraryId join WcfServiceConfig c on s.Id = c.WcfServiceId where s.IsAdmin=0";

        private const string SchedulerSql =
            "select l.id, l.filepath, s.TimerId, s.TimerAssemblyName, s.TimerClassName, c.CronExpression, c.JobId, c.GroupId, c.TriggerId from TimerLibrary l join TimerJob s on l.Id = s.TimerLibraryId join TimerConfig c on s.Id = c.TimerId";

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
                log.Error(e);
                throw;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }
            return wcfServiceLibraries;
        }

        public new List<ITimerLibrary> GetTimerLibraries()
        {
            var timerLibraries = new List<ITimerLibrary>();
            ITimerLibrary timerLibrary = null;
            string id = string.Empty;

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    using (var command = new SQLiteCommand(SchedulerSql, connection) { CommandType = CommandType.Text })
                    {
                        connection.Open();

                        SQLiteDataReader rd = command.ExecuteReader();

                        while (rd.Read())
                        {
                            if (id != rd[0].ToString())
                            {
                                id = rd[0].ToString();
                                string filePath = rd[1].ToString();
                                timerLibrary = TimerLibrary.Create(filePath);

                                timerLibraries.Add(timerLibrary);
                            }

                            ITimerJob timer = TimerJob.Create(
                                rd[2].ToString(),
                                rd[3].ToString(),
                                rd[4].ToString());

                            ITimerConfig timerConfig = TimerConfig.Create(
                                rd[5].ToString(),
                                rd[7].ToString(),
                                rd[8].ToString(),
                                rd[6].ToString(),
                                timer);

                            if (timerLibrary == null ||
                                timer == null ||
                                timerConfig == null)
                                throw new Exception("Problem reading from the configuration database");

                            timerLibrary.TimerConfigs.Add(timerConfig);
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                log.Error(e);
                throw;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }
            return timerLibraries;
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
                log.Error("");
                log.Error("************************************************************");
                log.Error(
                    "PROBLEM READING FROM SQLite DATABASE.  Please read the INSTALLATION-README.txt for instructions on how to setup access to the SQLite database");
                log.Error("************************************************************");
                log.Error("");
                
                log.Error(e);
            }
            catch (Exception e)
            {
                log.Error(e);
            }

            return wcfServiceConfig;
        }

        #endregion
    }
}