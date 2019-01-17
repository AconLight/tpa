using Data.ModelTree;

namespace Data
{
    public interface DataBridgeInterface
    {
        void write(LogicModelTreeHandler tree);
        LogicModelTreeHandler read();
    }
}
