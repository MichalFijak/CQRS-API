using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptor
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if(eventData.Context == null)
            {
                return ValueTask.FromResult(result);
            }

            foreach( var entry in eventData.Context.ChangeTracker.Entries<ISoftDelete>())
            {
                if(entry.State != EntityState.Deleted)
                {
                    continue;
                }
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = DateTime.UtcNow;
            }

            return ValueTask.FromResult(result);
        }
    }
}
