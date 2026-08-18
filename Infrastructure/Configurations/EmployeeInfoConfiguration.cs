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

        }
    }
}
