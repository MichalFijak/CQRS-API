using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; } = null!;
        public DbSet<EmployeeInfo> EmployeeInfo { get; set; } = null!;
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<EmployeeInfo>().ToTable("EmployeeInfo");

            base.OnModelCreating(modelBuilder);
        }

    }
}
