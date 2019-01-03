using Reflection.Model;

namespace Reflection
{
    public interface DataBridgeInterface
    {
        void write(AssemblyMetaData assemblyMetaData);
        AssemblyMetaData read();
    }
}
