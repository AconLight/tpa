using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.SerializationModelTree
{
    [DataContract(IsReference = true)]
    public class ModelNode
    {
        public ObservableCollection<ModelNode> allNodes { get; set; }
        [DataMember]
        public string TypeName;
        public ModelNode Parent;
        [DataMember]
        public string Name { get; set; }



        public ModelNode(ModelNode Parent)
        {
            this.Parent = Parent;
            allNodes = new ObservableCollection<ModelNode>();
        }

        public void tryLoad(List<ModelNode> loadedNodes)
        {
            foreach(ModelNode node in loadedNodes)
            {
                if (node.TypeName == TypeName && node.Name == Name)
                {
                    return;
                }
            }
            loadedNodes.Add(this);
            Load(loadedNodes);
        }

        public virtual void Load(List<ModelNode> loadedNodes)
        {
            // do nothing
        }
        public virtual void loadAll()
        {
            // do nothing
        }
    }
}
