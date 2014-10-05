using System;
using System.Collections.Generic;

namespace AkamaiPurgeConsole.CommandLine
{
    public class Option
    {
        private readonly List<string> _aliases = new List<string>();
        private string _name = "";
        private string _description = "";

        public Option()
        {
            _name = "default";
            _description = "Default Option";
            IsRequired = false;
            IsMatched = false;
            NeedsValue = false;
            IsDefault = true;
        }

        public Option(string name, string description, Type type, bool hasval, bool required)
        {
            _description = description;
            Type = type;
            IsRequired = required;
            IsMatched = false;
            NeedsValue = hasval;
            IsDefault = false;

            Initialise(name);
        }

        private void Initialise(string name)
        {
            _name = name;
            AddAliase(name);
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Type Type { get; private set; }
        public bool NeedsValue { get; private set; }
        public bool IsRequired { get; private set; }
        public bool IsMatched { get; set; }
        public string Value { get; set; }
        public bool IsDefault { get; private set; }

        public string[] Names
        {
            get
            {
                return _aliases.ToArray();
            }
        }

        public string[] Aliases
        {
            get
            {
                var list = new List<string>(_aliases);
                list.Remove(_name);
                return list.ToArray();
            }
        }

        public void AddAliase(string aliase)
        {
            _aliases.Add(aliase);
        }


    }
}
