using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataConnectionEntity.Entities; 


namespace DataConnectionEntity
{
    public partial class DataModelContext : DbContext
    {
        public DataModelContext()
            : base("name=DataModelContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        static DataModelContext()
        {
            Database.SetInitializer(new NullDatabaseInitializer<DataModelContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            DataModelMapping.Configure(modelBuilder);

            modelBuilder.Entity<Contact>()
            .HasKey(o => o.Id);

        }
        
        public DbSet<Contact> Contacts { get; set; }

        



     
    }
}
