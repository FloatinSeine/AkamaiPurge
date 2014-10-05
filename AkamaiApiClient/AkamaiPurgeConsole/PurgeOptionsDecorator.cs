using System;
using System.Collections.Generic;
using System.Linq;
using AkamaiApiProxy;
using AkamaiPurgeConsole.CommandLine;

namespace AkamaiPurgeConsole
{
    public class PurgeOptionsDecorator : OptionsDecorator
    {
        private readonly List<Option> _options;

        public PurgeOptionsDecorator(List<Option> options)
        {
            _options = options;
        }

        public PurgeOptions Decorate()
        {
            var opts = new PurgeOptions
            {
                Email = FetchOption("Email").Value,
                Domain = ConvertToDomainType(FetchOption("Domain").Value),
                Action = ConvertToActionType(FetchOption("Action").Value),
                Type = ConvertToPurgeType(FetchOption("Type").Value),
                File = FetchOption("File").Value
            };

            return opts;
        }

        private Option FetchOption(string name)
        {
            return _options.First(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        private static DomainType ConvertToDomainType(string value)
        {
            DomainType tenum;

            if (Enum.TryParse(value, true, out tenum) != true)
            {
                tenum = DomainType.Staging;
            }
            return tenum;
        }

        private static ActionType ConvertToActionType(string value)
        {
            ActionType tenum;

            if (Enum.TryParse(value, true, out tenum) != true)
            {
                tenum = ActionType.Invalidate;
            }
            return tenum;
        }

        private static PurgeType ConvertToPurgeType(string value)
        {
            PurgeType tenum;

            if (Enum.TryParse(value, true, out tenum) != true)
            {
                tenum = PurgeType.Arl;
            }
            return tenum;
        }
    }
}
