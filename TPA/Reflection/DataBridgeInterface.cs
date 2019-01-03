using Reflection.Model;
using Reflection.ModelTree;

namespace Reflection
{
    public interface DataBridgeInterface
    {
        void write(ModelNodeAssembly assemblyMetaData);
        ModelNodeAssembly read();
    }
}
