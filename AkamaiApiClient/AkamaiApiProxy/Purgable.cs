using System.Collections.Generic;
using AkamaiApiProxy.AkamaiPurgeApi;

namespace AkamaiApiProxy
{
    public interface Purgable
    {
        List<PurgeResult> Purge(PurgeOptions options, string[] resources);
    }
}
