using System;
using System.Collections.Generic;

namespace AkamaiApiProxy
{
    public enum ActionType
    {
        Remove,
        Invalidate
    }

    public enum DomainType
    {
        Production,
        Staging
    }

    public enum PurgeType
    {
        Arl,
        CpCode
    }

    public class PurgeOptions
    {
        public PurgeOptions()
        {
            Domain = DomainType.Staging;
            Action = ActionType.Invalidate;
            Type = PurgeType.Arl;
            Email = string.Empty;
        }

        public DomainType Domain
        {
            get;
            set;
        }

        public ActionType Action
        {
            get;
            set;
        }

        public PurgeType Type
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string File
        {
            get;
            set;
        }

        public string[] Options
        {
            get
            {
                var opts = new List<string>(3) {ToString(Action), 
                                                ToString(Domain), 
                                                ToString(Type)};
                if (string.IsNullOrEmpty(Email) != true)
                {
                    opts.Add("email-notification=" + Email);
                }
                return opts.ToArray();
            }
        }

        private static string ToString(ActionType type)
        {
            return ("action=" + ((type == ActionType.Remove) ? "remove" : "invalidate"));
        }
        private static string ToString(DomainType type)
        {
            return ("domain=" + ((type == DomainType.Staging) ? "staging" : "production"));
        }
        private static string ToString(PurgeType type)
        {
            return ("type=" + ((type == PurgeType.Arl) ? "arl" : "cpcode"));
        }
    }
}
