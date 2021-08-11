using BookManagementModels.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementModels.Entities
{
    public class BookManagementContext : DbContext
    {
        public string ConnectionString { get; set; }
   
        public BookManagementContext()
        {

        }

        public BookManagementContext(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }


        /// <summary>
        /// set entity configurations
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BooksConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);

        }
    }


   
}
