using System;
using log4net;
using log4net.Config;

namespace AkamaiApiProxy.Instrumentation
{
    public class Logger : Logable
    {
        static Logger()
        {
            XmlConfigurator.Configure();
        }

        public void Info(string message)
        {
            var log = LogManager.GetLogger("InfoLogger");
            log.Info(message);
        }
        public void Warn(string message)
        {
            var log = LogManager.GetLogger("InfoLogger");
            log.Warn(message);
        }

        public void Debug(string message)
        {
            var log = LogManager.GetLogger("DetailsLogger");
            log.Debug(message);
        }

        public void Error(string message)
        {
            var log = LogManager.GetLogger("DetailsLogger");
            log.Error(message);
        }

        public void Fatal(string message)
        {
            var log = LogManager.GetLogger("DetailsLogger");
            log.Error(message);
        }
    }
}
