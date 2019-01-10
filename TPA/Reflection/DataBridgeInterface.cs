using Reflection.Model;
using Reflection.ModelTree;

namespace Reflection
{
    public interface DataBridgeInterface
    {
        void write(LogicModelTreeHandler tree);
        LogicModelTreeHandler read();
    }
}
