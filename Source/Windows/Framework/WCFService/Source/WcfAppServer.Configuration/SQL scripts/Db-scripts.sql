-- create WcfAppServerConfiguration.s3db database


CREATE TABLE [WcfService] (
[Id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
[WcfServiceLibraryId] int NOT NULL,
[ServiceId] varchar(100)  UNIQUE NOT NULL,
[ServiceAssemblyName] varchar(100)  NOT NULL,
[ServiceClassName] varchar(100)  NOT NULL,
[ContractAssemblyName] varchar(100)  NOT NULL,
[ContractClassName] varchar(100)  NOT NULL
);

CREATE TABLE [WcfServiceConfig] (
[Id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
[WcfServiceId] integer  NOT NULL,
[Address] varchar(100)  NOT NULL,
[Binding] varchar(10)  NOT NULL,
[IncludeMex] bit DEFAULT '0' NOT NULL,
[IncludeExceptionDetailInFault] bit DEFAULT '0' NOT NULL
);

CREATE TABLE [WcfServiceLibrary] (
[Id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
[FilePath] VARCHAR(200)  UNIQUE NULL
);

insert into wcfservicelibrary 
  (filepath)
select 
  '\\127.0.0.1\Assemblies\WcfServiceLibrary1.dll';
insert into wcfservicelibrary
  (filepath)
select 
  '\\127.0.0.1\Assemblies\WcfServiceLibrary2.dll';

insert into WcfService
   (WcfServiceLibraryId,
   ServiceId,
   ServiceAssemblyName,
   ServiceClassName,
   ContractAssemblyName,
   ContractClassName,
   IsAdmin)
 select
   0,
   "WCF App Server Admin Service",
   "WcfAppServer",
   "AppServer",
   "WcfAppServer",
   "IAppServer",
   1;

insert into WcfServiceConfig
  (WcfServiceId,
  Address,
  IncludeMex,
  IncludeExceptionDetailInFault)
select
  1,
  'net.tcp://localhost:8050/WcfAppServer/AppServer',
  0,
  1;


insert into WcfService
   (WcfServiceLibraryId,
   ServiceId,
   ServiceAssemblyName,
   ServiceClassName,
   ContractAssemblyName,
   ContractClassName,
   IsAdmin)
 select
   1,
   "Service#1",
   "WcfServiceLibrary1",
   "Service1",
   "WcfServiceLibrary1",
   "IService1",
   0;

insert into WcfServiceConfig
  (WcfServiceId,
  Address,
  IncludeMex,
  IncludeExceptionDetailInFault)
select
  2,
  "net.tcp://localhost:8731/WcfServiceLibrary1/Service1",
  0,
  1;


insert into WcfService
   (WcfServiceLibraryId,
   ServiceId,
   ServiceAssemblyName,
   ServiceClassName,
   ContractAssemblyName,
   ContractClassName,
   IsAdmin)
 select
   1,
   "Service#2",
   "WcfServiceLibrary2",
   "Service1",
   "WcfServiceLibrary2",
   "IService1",
   0;

insert into WcfServiceConfig
  (WcfServiceId,
  Address,
  IncludeMex,
  IncludeExceptionDetailInFault)
select
  3,
  "http://localhost:8732/WcfServiceLibrary1/Service2",
  0,
  1;


insert into WcfService
   (WcfServiceLibraryId,
   ServiceId,
   ServiceAssemblyName,
   ServiceClassName,
   ContractAssemblyName,
   ContractClassName,
   IsAdmin)
 select
   2,
   "Service#3",
   "WcfServiceLibrary1",
   "Service2",
   "WcfServiceLibrary1",
   "IService2",
   0;


insert into WcfServiceConfig
  (WcfServiceId,
  Address,
  IncludeMex,
  IncludeExceptionDetailInFault)
select
  4,
  "http://localhost:8733/WcfServiceLibrary2/Service1",
  0,
  1;