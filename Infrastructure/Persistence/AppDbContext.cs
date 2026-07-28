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


            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    Username = "John",
                    Salary = 5000,
                    Email = "john@example.com"
                },
                new Employee
                {
                    EmployeeId = 2,
                    Username = "Anna",
                    Salary = 6200,
                    Email = "anna@example.com"
                }
            );

            modelBuilder.Entity<EmployeeInfo>().HasData(
                new EmployeeInfo
                {
                    EmployeeId = 1,
                    Collegue = "Mike",
                    Department = "IT",
                    Position = "Developer",
                    IsPromoted = false
                }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
