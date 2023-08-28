using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Authentication.Persistence
{
    public class UserStoreContext : DbContext
    {
        public UserStoreContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
    }
}
