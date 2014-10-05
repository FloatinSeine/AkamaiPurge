namespace AkamaiApiProxy.Instrumentation
{
    public interface Logable
    {
        void Info(string message);
        void Warn(string message);
        void Debug(string message);
        void Error(string message);
        void Fatal(string message);
    }
}
