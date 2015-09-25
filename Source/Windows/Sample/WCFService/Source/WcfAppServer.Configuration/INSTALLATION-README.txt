PREREQUISITES:  Please ensure you have ADO.NET for SQLite installed and SQLite administrator tool

When installing the VN Application Server you need to carry out the following post build steps.

Connect to your newly installed WcfAppServerConfiguration.s3db (using SQLite Administrator tool) 
and update the data in the following tables to remove the example services, replacing them 
with your own service information:
  
  WcfServiceLibrary, 
  WcfService, 
  WcfServiceConfig

Finally, update the app.config file to state the full path to your WcfAppServerConfiguration.s3db file.  
This is because the service will be hosted within System32 so a relative path will not work.