using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementModels.Entities.Configuration
{
    /// <summary>
    /// Desingn Time Context Configuration for Migrations
    /// </summary>
    public class BookManagementContextFactory : IDesignTimeDbContextFactory<BookManagementContext>
    {
        public BookManagementContext CreateDbContext(string[] args)
        {            
            return new BookManagementContext(@"Host=localhost;Database=BookManagement;Username=postgres;Password=1234qqqQ");
        }
    }
}
