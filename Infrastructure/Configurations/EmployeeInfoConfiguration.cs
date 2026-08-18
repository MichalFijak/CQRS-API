using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configurations
{
    public class EmployeeInfoConfiguration : IEntityTypeConfiguration<Domain.Entities.EmployeeInfo>
    {
        public void Configure(EntityTypeBuilder<EmployeeInfo> builder)
        {

            builder.HasKey(e => e.EmployeeId);
            builder.HasQueryFilter("SoftDelete", e => !e.IsDeleted);
            builder.HasIndex(e=> new { e.IsDeleted });

        }
    }
}
