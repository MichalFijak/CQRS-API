using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> AsQueryable();
        IQueryable<Employee> GetEmployeeQuery();
        IQueryable<Employee> GetDeletedEmployeeQuery();
        IQueryable<Employee> QueryAll();

        Task AddAsync(Employee user, CancellationToken ct);
        Task DeleteAsync(Employee user, CancellationToken ct);
        Task<Employee?> GetByIdAsync(int id, CancellationToken ct);
        Task UpdateAsync(Employee user, CancellationToken ct);
    }

}