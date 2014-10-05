using StructureMap;

namespace AkamaiPurgeConsole.Configuration.IoC
{
    public interface Bindable
    {
        void Bind(IContainer container);
    }
}
