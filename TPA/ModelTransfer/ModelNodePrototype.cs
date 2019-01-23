using System.Collections.Generic;

namespace ModelTransfer
{
    public class ModelNodePrototype
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
        public ModelNodePrototype Parent { get; set; }
        public List<ModelNodePrototype> Nodes;

        public virtual void OnCreate()
        {
            //do nothing
        }
        public virtual void OnLoad()
        {
            //do nothing
        }
    }
}
