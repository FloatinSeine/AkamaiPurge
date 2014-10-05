using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AkamaiPurgeConsole.IO
{
    public class FileReader : Readable
    {
        public string[] Read(string filepath)
        {
            var list = new List<string>();
            if (File.Exists(filepath) == true)
            {
                using (var f = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    TextReader r = new StreamReader(f, new UTF8Encoding());
                    while (r.Peek() > 0)
                    {
                        list.Add(r.ReadLine());
                    }
                    r.Close();
                }                
            }

            return list.ToArray();
        }
    }
}
