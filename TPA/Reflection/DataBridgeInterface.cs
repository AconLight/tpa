
using ModelTransfer;
using Reflection.Model;

namespace Reflection
{
    public interface DataBridgeInterface
    {
        void save(AssemblyMetaData assembly);
        ModelNodePrototype load();
    }
}
