using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Configurations;
using Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            EmployeeSeed.Apply(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries<ISoftDelete>().Where(e=>e.State==EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
                entry.CurrentValues["DeletedAt"] = DateTime.UtcNow;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    
    }
}