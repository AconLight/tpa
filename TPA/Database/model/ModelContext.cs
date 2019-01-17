using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Database.model
{
    class ModelContext : DbContext
    {
        public DbSet<DBModelNode> nodes { get; set; }
    }
}
