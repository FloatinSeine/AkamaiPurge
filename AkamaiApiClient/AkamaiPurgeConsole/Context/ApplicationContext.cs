using AkamaiApiProxy;
using AkamaiApiProxy.Instrumentation;
using AkamaiPurgeConsole.CommandLine;

namespace AkamaiPurgeConsole.Context
{
    public interface ApplicationContext
    {
        Logable Logger { get; }
        Purgable PurgeApi { get; }
        Parsable CreateCommandLineParser();
    }
}
