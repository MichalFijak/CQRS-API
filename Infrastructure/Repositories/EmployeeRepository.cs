
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class EmployeeRepository : IEmployeeRepository
    {

        private readonly AppDbContext context;

        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Employee> AsQueryable() => context.Employee.AsQueryable();

        public async Task<Employee?> GetByIdAsync(int id, CancellationToken ct)
            => await context.Employee.FirstOrDefaultAsync(u => u.EmployeeId == id, ct);

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken ct)
            => await context.Employee.ToListAsync(ct);

        public async Task AddAsync(Employee employee, CancellationToken ct)
        {
            await context.Employee.AddAsync(employee, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Employee employee, CancellationToken ct)
        {
            context.Employee.Update(employee);
            await context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Employee employee, CancellationToken ct)
        {
            context.Employee.Remove(employee);
            await context.SaveChangesAsync(ct);
        }
    }
}
