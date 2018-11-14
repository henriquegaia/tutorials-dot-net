using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Books.Entities;

namespace Books.Web.DataContexts
{
    public class BooksDb : DbContext
    {
        public BooksDb()
            : base("DefaultConnection")
        {
            /*
             * To view logging queries:
             * 
             * 1-Debug > Start Debugging
             * 2-Debug > Windows > Output
             */
            Database.Log = sql => Debug.Write(sql);
        }

        // Schemas

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("library");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
    }
}