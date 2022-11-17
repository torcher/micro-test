using hhe_service.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hhe_service.Data
{
    public class HHEDbContext : DbContext
    {
        public DbSet<HHE> HHEs { get; set; }
        public string DbPath { get; }

        public HHEDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "hhes.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
