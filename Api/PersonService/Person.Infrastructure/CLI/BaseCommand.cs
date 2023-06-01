using Repository;
using System.CommandLine;

namespace Person.Infrastructure.CLI
{
    public abstract class BaseCommand:Command
    { 
        public BaseCommand(string name,string desc):base(name, desc)
        {
 
        }
    }
}
