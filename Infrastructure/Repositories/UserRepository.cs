
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {

        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<User> AsQueryable() => context.Users.AsQueryable();

        public async Task<User?> GetByIdAsync(int id, CancellationToken ct)
            => await context.Users.FirstOrDefaultAsync(u => u.UserId == id, ct);

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct)
            => await context.Users.ToListAsync(ct);

        public async Task AddAsync(User user, CancellationToken ct)
        {
            await context.Users.AddAsync(user, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(User user, CancellationToken ct)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(User user, CancellationToken ct)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync(ct);
        }
    }
}
