using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmployeeInfoRepository
    {
        IQueryable<EmployeeInfo> AsQueryable();
        Task AddAsync(EmployeeInfo userInfo, CancellationToken ct);
        Task DeleteAsync(EmployeeInfo userInfo, CancellationToken ct);
        Task<EmployeeInfo?> GetByIdAsync(int id, CancellationToken ct);
        Task UpdateAsync(EmployeeInfo userInfo, CancellationToken ct);
    }
}