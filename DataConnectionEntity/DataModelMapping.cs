using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataConnectionEntity.Entities;

namespace DataConnectionEntity
{
    public class DataModelMapping
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            MapContact(modelBuilder);
        }


        static void MapContact(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().ToTable("Contacts");

        }

    }

}

