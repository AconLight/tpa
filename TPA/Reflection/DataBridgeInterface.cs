
using ModelTransfer;
using Reflection.Model;

namespace Reflection
{
    public interface DataBridgeInterface
    {
        void save(ModelNodePrototype assembly);
        ModelNodePrototype load();
    }
}
