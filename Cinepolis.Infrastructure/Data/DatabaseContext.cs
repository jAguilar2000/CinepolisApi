using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Data
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
