using Microsoft.EntityFrameworkCore;
using TeacherManagementApi.Models;

namespace TeacherManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Teacher> Teachers => Set<Teacher>();
    }
}