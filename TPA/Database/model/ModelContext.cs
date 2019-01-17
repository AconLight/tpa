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

        public ModelContext() : base("siemadb")
        {
        }
        public DbSet<DBModelNode> nodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
