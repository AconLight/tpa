using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.ModelTree
{
    public class LogicModelNode
    {
        public Boolean isLooped;
        public ObservableCollection<LogicModelNode> loopedNodes { get; set; }
        public ObservableCollection<LogicModelNode> allNodes { get; set; }
        public string TypeName;
        public LogicModelNode Parent;
        public string Name { get; set; }



        public LogicModelNode(LogicModelNode Parent)
        {
            isLooped = false;
            this.Parent = Parent;
            allNodes = new ObservableCollection<LogicModelNode>();
            loopedNodes = new ObservableCollection<LogicModelNode>();
        }

        public void tryLoad(List<LogicModelNode> loadedNodes)
        {
            foreach(LogicModelNode node in loadedNodes)
            {
                if (node.TypeName == TypeName && node.Name == Name)
                {
                    allNodes.Clear();
                    //Console.WriteLine(node.allNodes.Count());
                    foreach (LogicModelNode n in node.allNodes)
                    {
                        allNodes.Add(n);
                    }
                    
                    isLooped = true;
                    return;
                }
            }
            loadedNodes.Add(this);
            Load(loadedNodes);
            //loadAll();

        }

        public virtual void Load(List<LogicModelNode> loadedNodes)
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
