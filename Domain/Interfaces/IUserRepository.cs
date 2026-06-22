using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> AsQueryable();

        Task AddAsync(User user, CancellationToken ct);
        Task DeleteAsync(User user, CancellationToken ct);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken ct);
        Task<User?> GetByIdAsync(int id, CancellationToken ct);
        Task UpdateAsync(User user, CancellationToken ct);
    }

}