using Microsoft.EntityFrameworkCore;
using ThiC6.Models;

namespace ThiC6.Data
{
    public class QLSVDbContext:DbContext
    {
        public QLSVDbContext(DbContextOptions options) : base(options) { }
        public DbSet<SinhVien> SinhViens { get; set; }
    }
}
