using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<EmployeeInfo> EmployeeInfos { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
modelBuilder.Entity<Employee>()
    .HasOne(e => e.EmployeeInfo)
    .WithOne(i => i.Employee)
    .HasForeignKey<EmployeeInfo>(i => i.EmployeeId);

            var firstNames = new[]
            {
        "James","John","Robert","Michael","William","David","Richard","Joseph","Thomas","Charles",
        "Mary","Patricia","Jennifer","Linda","Elizabeth","Barbara","Susan","Jessica","Sarah","Karen"
    };

            var lastNames = new[]
            {
        "Smith","Johnson","Williams","Brown","Jones","Garcia","Miller","Davis","Rodriguez","Martinez",
        "Hernandez","Lopez","Gonzalez","Wilson","Anderson","Thomas","Taylor","Moore","Jackson","Martin"
    };

            var departments = new[]
            {
        "IT", "HR", "Finance", "Logistics", "Marketing"
    };

            var positions = new[]
            {
        "Junior Developer", "Mid Developer", "Senior Developer",
        "Business Analyst", "HR Specialist", "Project Manager",
        "Coordinator", "System Administrator", "UI/UX Designer", "Consultant"
    };

            var employees = new List<Employee>();
            var infos = new List<EmployeeInfo>();

            var rnd = new Random(456);

            for (int i = 1; i <= 100; i++)
            {
                var first = firstNames[rnd.Next(firstNames.Length)];
                var last = lastNames[rnd.Next(lastNames.Length)];

                employees.Add(new Employee
                {
                    EmployeeId = i,
                    Username = $"{first} {last}",
                    Salary = rnd.Next(3500, 12000),
                    Email = $"{first.ToLower()}.{last.ToLower()}@example.com"
                });

                infos.Add(new EmployeeInfo
                {
                    EmployeeId = i,
                    Collegue = $"{firstNames[rnd.Next(firstNames.Length)]} {lastNames[rnd.Next(lastNames.Length)]}",
                    Department = departments[rnd.Next(departments.Length)],
                    Position = positions[rnd.Next(positions.Length)],
                    IsPromoted = rnd.Next(0, 2) == 1
                });
            }

            modelBuilder.Entity<Employee>().HasData(employees);
            modelBuilder.Entity<EmployeeInfo>().HasData(infos);

            base.OnModelCreating(modelBuilder);
        }

    }
}
