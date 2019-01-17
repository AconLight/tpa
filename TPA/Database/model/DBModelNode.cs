using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.model
{
    class DBModelNode
    {
        public String TypeName { get; set; }
        public String Name { get; set; }
        public DBModelNode Parent { get; set; }

        public virtual List<DBModelNode> children { get; set; }
    }
}
