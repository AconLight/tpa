using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.model
{
    public class DBModelNode
    {
        [Key] public int DBModelNodeID { get; set; }
        public Boolean isLooped { get; set; }
        public String TypeName { get; set; }
        public String Name { get; set; }
        public DBModelNode Parent { get; set; }

        public virtual List<DBModelNode> children { get; set; }
    }
}
