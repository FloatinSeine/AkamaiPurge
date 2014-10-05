using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AkamaiPurgeConsole.CommandLine;
using AkamaiPurgeConsole.Configuration.IoC;
using AkamaiPurgeConsole.Context;

namespace AkamaiPurgeConsole
{
    class Program
    {
        internal static ApplicationContext Context;

        [STAThread]
        static void Main(string[] args)
        {
            Context = StructuremapActivator.Activatge();

            Context.Logger.Info("Application Start");

            var parser = Context.CreateCommandLineParser();
            parser.Parse(args);

            if (parser.HasErrors)
            {
                Context.Logger.Warn("Command Line Errors");
                //log errors
            }
            if (parser.ShowHelp)
            {
                Context.Logger.Warn("Show Help");
                ShowHelp(parser);
            }

            if (parser.HasErrors != true && parser.ShowHelp != true)
            {
                StartPurge(parser.Options);
            }

            Context.Logger.Info("Application Completed");
            //var readLine = System.Console.In.ReadLine();
        }

        private static void StartPurge(List<Option> options)
        {
            Context.Logger.Info("Intialise Purger");

            var purger = new AkamaiPurger(options, Context.Logger, Context.PurgeApi);
            purger.Start();
            while (purger.IsCompleted != true)
            {
                if (purger.Status != TaskStatus.Running)
                {
                    Context.Logger.Warn("running " + purger.Status.ToString());
                }
            }
        }

        //console.exe -d=production -t=arl -a=invalidate -e="recipient@company.com" -f="c:\\path\\arls.txt"
        //console.exe -?
        private static void ShowHelp(Parsable parser)
        {
            Context.Logger.Warn(string.Empty + "\n");
            foreach (var o in parser.Options)
            {
                Context.Logger.Warn(o.Description);
            }
            Context.Logger.Warn(string.Empty+"\n");
            Context.Logger.Warn(parser.GetHelpText());
        }
    }

}
