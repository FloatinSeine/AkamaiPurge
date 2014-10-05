using AkamaiApiProxy;
using AkamaiApiProxy.Instrumentation;
using AkamaiPurgeConsole.CommandLine;
using StructureMap;

namespace AkamaiPurgeConsole.Context
{
    
    public class PurgeContext : ApplicationContext
    {
        private readonly Logable _logger;
        private readonly Purgable _purgable;
        private readonly IContainer _container;

        public PurgeContext(IContainer container, Logable logger, Purgable purgable)
        {
            _container = container;
            _logger = logger;
            _purgable = purgable;
        }

        public Logable Logger
        {
            get
            {
                return _logger;
            }
        }

        public Purgable PurgeApi
        {
            get
            {
                return _purgable;
            }
        }

        public Parsable CreateCommandLineParser()
        {
            return _container.GetInstance(typeof(Parsable)) as Parsable;
        }
    }
}
