using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserInfoRepository
    {
        IQueryable<UserInfo> AsQueryable();
        Task AddAsync(UserInfo userInfo, CancellationToken ct);
        Task DeleteAsync(UserInfo userInfo, CancellationToken ct);
        Task<UserInfo?> GetByIdAsync(int id, CancellationToken ct);
        Task UpdateAsync(UserInfo userInfo, CancellationToken ct);
    }
}