Please ensure you are running Visual Studio with Administrator rights.

To run the demo, set both the ConsoleHost and ConsoleClient projects as startup projects.

You can do this by right clicking on the Solution file, 
select properties, multiple startup projects, 
then set the two Consoles projects "action" to start

NOTE: 
1)  You must enable the "Net. Tcp Port Sharing Service" service on the host machine to run the demo.
2)  When directly running the WcfAppServer.ConsoleHost.exe and WcfAppServer.ConsoleClient.exe please 
run as administrator