using Microsoft.EntityFrameworkCore;

namespace SuperDairy
{
    public class DBCtx : DbContext
    {
        public DBCtx(DbContextOptions<DBCtx> options) : base(options)
        {
        }
        public DbSet<Core.Model.User> User { get; set; }
    }
   
}
