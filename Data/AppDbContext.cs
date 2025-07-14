using Microsoft.EntityFrameworkCore;
using ReservasiRuangApp.Models;

namespace ReservasiRuangApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Reservasi> Reservasi { get; set; }
    }
}
