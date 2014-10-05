using System.Collections.Generic;
using System.Linq;
using AkamaiApiProxy.AkamaiPurgeApi;
using AkamaiApiProxy.Configuration;
using AkamaiApiProxy.Helper;
using AkamaiApiProxy.Instrumentation;

namespace AkamaiApiProxy
{
    public class PurgeApi : Purgable
    {
        private const string Network = ""; // Depricated in API
        private readonly Logonable _config = AkamaiSecurityConfigurationSettings.Settings;
        private readonly Logable _logger;

        public PurgeApi(Logable logger)
        {
            _logger = logger;
        }

        public List<PurgeResult> Purge(PurgeOptions options, string[] resources)
        {
            var results = new List<PurgeResult>();
            if (ValidateResoruces(resources) != true)
            {
                results.Add(new PurgeResult {estTime = 0, resultMsg = "Error Validating resources", resultCode = -1});
            }
            else
            {
                var splitter = new ListSplitter(_logger);
                var lists = splitter.Split(resources);

                results = lists.Select(list => RequestPurge(options, list)).ToList();
            }
            return results;
        }

        private PurgeResult RequestPurge(PurgeOptions options, string[] resources)
        {
            _logger.Info("Requesting Akamai Purge Resources");
            PurgeResult result = null;
            using (var client = new PurgeApiClient())
            {
                //result = (new PurgeResult { estTime = 0, resultMsg = "Completed Purge", resultCode = 100 });
                result = client.purgeRequest(_config.Username, _config.Password, Network, options.Options, resources);
            }
            return result;
        }

        private static bool ValidateResoruces(string[] resources)
        {
            return (resources.Length > 0);
        }

    }
}
