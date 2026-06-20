
using Domain.Entities;

public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken ct);
        Task DeleteAsync(User user, CancellationToken ct);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken ct);
        Task<User?> GetByIdAsync(int id, CancellationToken ct);
        Task UpdateAsync(User user, CancellationToken ct);
    }
