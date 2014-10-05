using AkamaiApiProxy;
using AkamaiApiProxy.Instrumentation;
using AkamaiPurgeConsole.CommandLine;
using AkamaiPurgeConsole.Context;
using StructureMap;

namespace AkamaiPurgeConsole.Configuration.IoC
{
    public class InfrastructureBinding : Bindable
    {
        public void Bind(IContainer container)
        {
            container.Configure(x =>
            {
                x.ForSingletonOf<ApplicationContext>().Use<PurgeContext>();
                x.ForSingletonOf<Logable>().Use<Logger>();
                x.ForSingletonOf<Parsable>().Use<CommandLineParser>();
                x.ForSingletonOf<Purgable>().Use<PurgeApi>();

            });
        }
    }
}
