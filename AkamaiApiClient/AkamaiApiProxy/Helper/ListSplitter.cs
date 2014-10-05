using System.Collections.Generic;
using AkamaiApiProxy.Configuration;
using AkamaiApiProxy.Instrumentation;

namespace AkamaiApiProxy.Helper
{
    public class ListSplitter
    {
        private readonly Logable _logger;
        private readonly Chunkable _chunkable = AkamaiSecurityConfigurationSettings.Settings;

        public ListSplitter(Logable logger)
        {
            _logger = logger;
        }

        public ListSplitter(Logable logger, Chunkable chunker)
        {
            _logger = logger;
            _chunkable = chunker;
        }

        public int ChunckSize
        {
            get
            {
                return _chunkable.ChunkSize;
            }
        }

        public List<string[]> Split(string[] list)
        {
            var sub = new List<string>();
            var sublists = new List<string[]>();
            var x = 0;
            foreach (var s in list)
            {
                if (s.Length > 400)
                {
                    _logger.Warn("Item Size > 400 may not purge: " + s);
                }

                sub.Add(list[x]);
                x++;

                if (x % _chunkable.ChunkSize == 0 || x == list.Length)
                {
                    _logger.Warn("Chunk Size = " + sub.Count.ToString());
                    sublists.Add(sub.ToArray());
                    sub.Clear();
                }
            }
            return sublists;
        }
    }
}
