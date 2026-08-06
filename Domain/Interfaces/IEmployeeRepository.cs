using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> AsQueryable();

        IQueryable<Employee> GetEmployeeQuery();

        Task AddAsync(Employee user, CancellationToken ct);
        Task DeleteAsync(Employee user, CancellationToken ct);
        Task<IEnumerable<Employee>> GetAllAsync(CancellationToken ct);
        Task<Employee?> GetByIdAsync(int id, CancellationToken ct);
        Task UpdateAsync(Employee user, CancellationToken ct);
    }

}