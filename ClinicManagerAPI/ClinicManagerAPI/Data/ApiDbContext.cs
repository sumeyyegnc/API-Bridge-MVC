using ClinicManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagerAPI.Data
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Hasta> Hastalar { get; set; }
        public DbSet<Ilac> Ilaclar { get; set; }
    }
}
