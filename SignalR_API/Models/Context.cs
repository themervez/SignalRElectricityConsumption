using Microsoft.EntityFrameworkCore;

namespace SignalR_API.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Electric> Electrics { get; set; }
    }
}
