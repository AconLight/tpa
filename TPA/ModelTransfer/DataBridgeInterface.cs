
using ModelTransfer;

namespace ModelTransfer
{
    public interface DataBridgeInterface
    {
        void save(ModelNodePrototype assembly);
        ModelNodePrototype load();
    }
}
