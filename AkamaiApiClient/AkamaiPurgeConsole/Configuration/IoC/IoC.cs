using System;
using StructureMap;

namespace AkamaiPurgeConsole.Configuration.IoC
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            }));
            InitialiseBindings(ObjectFactory.Container);

            return ObjectFactory.Container;
        }

        public static void InitialiseBindings(IContainer container)
        {
            (new InfrastructureBinding()).Bind(container);
        }
    }
}
