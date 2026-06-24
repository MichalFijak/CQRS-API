using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {

        private readonly AppDbContext context;

        public UserInfoRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<UserInfo> AsQueryable() => context.UsersInfo.AsQueryable();

        public async Task<UserInfo?> GetByIdAsync(int id, CancellationToken ct)
            => await context.UsersInfo.FirstOrDefaultAsync(u => u.UserId == id, ct);

        public async Task AddAsync(UserInfo userInfo, CancellationToken ct)
        {
            await context.UsersInfo.AddAsync(userInfo, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(UserInfo userInfo, CancellationToken ct)
        {
            context.UsersInfo.Update(userInfo);
            await context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(UserInfo userInfo, CancellationToken ct)
        {
            context.UsersInfo.Remove(userInfo);
            await context.SaveChangesAsync(ct);
        }
    }
}
