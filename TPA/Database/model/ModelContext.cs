using System.Data.Entity;

namespace Database.model
{
    class ModelContext : DbContext
    {
        public ModelContext(string connectionString) : base(connectionString)
        {
            //Configuration.LazyLoadingEnabled = true;
            //Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<DBModelNode> nodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
