using Ninject;
using Ninject.Modules;
using WcfAppServer.Common.Interfaces;
using WcfAppServer.Configuration;

namespace WcfAppServer.ConsoleClient
{
    public class IoC
    {
        private static IKernel _container;

        public static IKernel Container
        {
            get
            {
                if (_container == null)
                    _container = new StandardKernel(new IoCModule());

                return _container;
            }
        }
    }

    public class IoCModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<IConfigService>().To<HardCodedConfigService>();
            Bind<IConfigService>().To<SQLiteConfigService>();
        }
    }
}