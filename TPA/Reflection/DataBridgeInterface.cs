using Reflection.Model;

namespace Reflection
{
    public interface DataBridgeInterface
    {
        string Type { get; }
        void write(AssemblyMetaData assemblyMetaData);
        AssemblyMetaData read();
    }
}
