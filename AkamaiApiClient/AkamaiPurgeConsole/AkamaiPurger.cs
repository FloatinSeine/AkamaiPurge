using System.Collections.Generic;
using System.Threading.Tasks;
using AkamaiApiProxy;
using AkamaiApiProxy.AkamaiPurgeApi;
using AkamaiApiProxy.Instrumentation;
using AkamaiPurgeConsole.IO;
using AkamaiPurgeConsole.CommandLine;

namespace AkamaiPurgeConsole
{
    public class AkamaiPurger
    {
        private Task _runner;
        private readonly List<Option> _options;
        private readonly Purgable _purgable;
        private readonly PurgeOptionsDecorator _decorator = null;
        private readonly Logable _view;

        public AkamaiPurger(List<Option> options, Logable logger, Purgable purger)
        {
            _decorator = new PurgeOptionsDecorator(options);
            _view = logger;
            _options = options;
            _purgable = purger;
        }

        public TaskStatus Status
        {
            get
            {
                return _runner.Status;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return (_runner.IsCompleted || _runner.IsCanceled);
            }
        }

        public void Start()
        { 
            _runner = new Task(BeginPurge);
            _runner.Start();
        }

        public void BeginPurge()
        {
            _view.Info("Begin Purge");
            var po = _decorator.Decorate();
            var arls = ReadPurgeFile(po.File);

            _view.Warn("ARLS Length: " + arls.Length.ToString());
            if (arls.Length == 0)
            {
                _view.Warn("Nothing to purge");
            }

            RequestPurge(po, arls);
        }

        private void RequestPurge(PurgeOptions options, string[] arls)
        {
            var results = _purgable.Purge(options, arls);
            foreach (var result in results)
            {
                if (result.resultCode == 100)
                {
                    LogResultSuccess(result);
                }
                else
                {
                    LogResultFailure(result);
                }                
            }

        }

        private static string[] ReadPurgeFile(string filename)
        {
            Readable reader = new FileReader();
            return reader.Read(filename);
        }

        private void LogResultSuccess(PurgeResult result)
        {
            _view.Warn("Success Result Code " + result.resultCode);
            _view.Warn("Success Result Message " + result.resultMsg);
            if (result.modifiers != null)
            {
                foreach (var s in result.modifiers)
                {
                    _view.Warn("Result Modifier " + s);
                }
            }
            _view.Warn("Result Est Time " + result.estTime);
        }

        private void LogResultFailure(PurgeResult result)
        {
            _view.Error("Error Result Code " + result.resultCode);
            _view.Error("Error Result Message " + result.resultMsg);

        }

    }
}
