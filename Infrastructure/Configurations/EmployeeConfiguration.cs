using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");
            builder.HasIndex(e => new{ e.EmployeeId,e.IsDeleted});

            builder.HasOne(e => e.EmployeeInfo)
                   .WithOne(i => i.Employee)
                   .HasForeignKey<EmployeeInfo>(i => i.EmployeeId);
            builder.HasQueryFilter("SoftDelete", e => !e.IsDeleted);

        }
    }
}
