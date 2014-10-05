using System.Collections.Generic;
using AkamaiPurgeConsole.Context;
using StructureMap.Pipeline;

namespace AkamaiPurgeConsole.Configuration.IoC
{
    public static class StructuremapActivator
    {
        public static ApplicationContext Activatge()
        {
            var container = IoC.Initialize();
            var dic = new Dictionary<string, object>();
            dic.Add("container", container);
            return container.GetInstance(typeof(ApplicationContext), new ExplicitArguments(dic)) as ApplicationContext;
        }
    }
}
