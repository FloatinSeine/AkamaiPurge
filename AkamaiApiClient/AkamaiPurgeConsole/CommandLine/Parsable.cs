
using System.Collections.Generic;

namespace AkamaiPurgeConsole.CommandLine
{
    public interface Parsable
    {
        List<Option> Options { get; }
    
        bool HasErrors { get; }
        bool ShowHelp { get; }
    

        void Parse(string[] options);
        string GetHelpText();
    }
}
