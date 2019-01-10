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
    public class SerModelNode
    {
        [DataMember]
        public bool isLooped = false;
        public ObservableCollection<SerModelNode> allNodes { get; set; }
        [DataMember]
        public string TypeName;
        public SerModelNode Parent;
        [DataMember]
        public string Name { get; set; }



        public SerModelNode(SerModelNode Parent)
        {
            this.Parent = Parent;
            allNodes = new ObservableCollection<SerModelNode>();
        }

        public void tryLoad(List<SerModelNode> loadedNodes)
        {
            foreach(SerModelNode node in loadedNodes)
            {
                if (node.TypeName == TypeName && node.Name == Name)
                {
                    return;
                }
            }
            loadedNodes.Add(this);
            Load(loadedNodes);
        }

        public virtual void Load(List<SerModelNode> loadedNodes)
        {
            // do nothing
        }
        public virtual void loadAll()
        {
            // do nothing
        }

        public virtual void retriveNode()
        {
            // do nothing
        }

        public virtual void loadNodes()
        {
            // do nothing
        }
    }
}
