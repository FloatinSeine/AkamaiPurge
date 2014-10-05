using System;
using System.Collections.Generic;
using System.Linq;
using AkamaiApiProxy.Instrumentation;

namespace AkamaiPurgeConsole.CommandLine
{
    public class CommandLineParser : Parsable
    {
        private readonly List<Option> _parameters = new List<Option>();
        private readonly List<Option> _errors = new List<Option>();
        private readonly Logable _logger = null;

        public CommandLineParser(Logable logger)
        {
            _logger = logger;
            SetOptions();
        }

        public List<Option> MissingOptions
        {
            get
            {
                return _parameters.FindAll(x => x.IsMatched == false);
            }
        }

        public List<Option> ErrorOptions
        {
            get
            {
                return _errors;
            }
        }

        public List<Option> Options
        {
            get
            {
                return _parameters;
            }
        }


        public bool HasErrors
        {
            get
            {
                return (_errors.Count > 0);
            }
        }

        public bool ShowHelp
        {
            get
            {
                return _parameters.Any(x => x.Name.ToLower() == "help" & x.IsMatched);
            }
        }

        public void Parse(string[] options)
        {
            _logger.Info("Parsing Command Line Options");
            foreach (var option in options)
            {
                _logger.Warn("Option " + option);
                var items = option.Split('=');
                var param = MatchParameter(items[0]);
                param.IsMatched = (param.IsDefault != true);
                param.Value = items.Last();

                if (param.IsDefault)
                {
                    _logger.Error("Error Option Found: " + param.Name + "=" + param.Value);
                    _errors.Add(param);
                }
            }
        }

        private Option MatchParameter(string name)
        {
            return _parameters.FirstOrDefault(x => x.Names.Contains(name));
        }

        private void SetOptions()
        {
            AppendOption("Help", "Help: -h", null, false, false, new[] { "?", "-?", "h", "-h" });
            AppendOption("Email", "Email address to reply to when purging completes -e=\"someone@somewhere.com\"", typeof(string), true, false, new []{"e","-e"});
            AppendOption("Domain", "Domain -d=[stage | production]", typeof(string), true, true, new[] { "d", "-d" });
            AppendOption("Type", "Purge Type -t=[arl | cp]", typeof(string), true, true, new[] { "t", "-t" });
            AppendOption("Action", "Action -a=[invalidate | delete]", typeof(string), true, true, new[] { "a", "-a" });
            AppendOption("File", "File path containing list of ARLs to be purged -f=\"c:\\path\\file.txt\"", typeof(string), true, true, new[] { "f", "-f" });
        }

        private void AppendOption(string name, string desc, Type type, bool hasValue, bool isRequired, string[] aliases)
        {
            var opt = new Option(name, desc, type, hasValue, isRequired);
            if (aliases != null)
            {
                foreach (var aliase in aliases)
                {
                    opt.AddAliase(aliase);
                }
            }
            _parameters.Add(opt);
        }

        public string GetHelpText()
        {
            var s = string.Empty;
            s += "Examples:\n\nconsole.exe -d=production -t=arl -a=invalidate -e=\"your.name@company.com\" -f=\"c:\\path\\arls.txt\"\n";
            s += "\nconsole.exe -?";
            return s;
        }
    }
}
