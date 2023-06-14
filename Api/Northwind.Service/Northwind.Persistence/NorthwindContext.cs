using Microsoft.EntityFrameworkCore;

namespace Northwind.Persistence
{

    public class NorthwindContext:DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {

        }
    }
}
