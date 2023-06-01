using EFAdapter;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.CommandLine;

namespace BsCli.Commands
{
    public abstract class BaseCommand:Command
    {
        public IUOW UOW { get; set; }
        public BaseCommand(string name,string desc):base(name, desc)
        {
            //var connString = configuration["ConnectionString"];
            //if (string.IsNullOrEmpty(connString))
            //{
            //    throw new NullReferenceException("Connection string is null");
            //}
            //var dbContextOptions = new DbContextOptionsBuilder<PersonAppContext>().UseSqlServer(connString).Options;
            //var context = new PersonAppContext(dbContextOptions);
            //return new EFUnitOfWork(context);
            //UOW = new EFUnitOfWork(DB);
        }
    }
}
