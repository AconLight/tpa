using System.Data.Entity;

namespace Database.model
{
    class ModelContext : DbContext
    {

        public ModelContext() : base("siemadb5")
        {
        }
        public DbSet<DBModelNode> nodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
