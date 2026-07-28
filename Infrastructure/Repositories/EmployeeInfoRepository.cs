using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeInfoRepository : IEmployeeInfoRepository
    {

        private readonly AppDbContext context;

        public EmployeeInfoRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<EmployeeInfo> AsQueryable() => context.EmployeeInfo.AsQueryable();

        public async Task<EmployeeInfo?> GetByIdAsync(int id, CancellationToken ct)
            => await context.EmployeeInfo.FirstOrDefaultAsync(u => u.EmployeeId == id, ct);

        public async Task AddAsync(EmployeeInfo userInfo, CancellationToken ct)
        {
            await context.EmployeeInfo.AddAsync(userInfo, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(EmployeeInfo userInfo, CancellationToken ct)
        {
            context.EmployeeInfo.Update(userInfo);
            await context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(EmployeeInfo userInfo, CancellationToken ct)
        {
            context.EmployeeInfo.Remove(userInfo);
            await context.SaveChangesAsync(ct);
        }
    }
}
