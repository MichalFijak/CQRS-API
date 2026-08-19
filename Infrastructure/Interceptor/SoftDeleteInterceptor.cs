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


        public static void CascadeSoftDelete(DbContext dbContext, ISoftDelete parentEntity)
        {
            var parentEntry = dbContext.Entry(parentEntity);

            foreach(var navigation in parentEntry.Navigations)
            {
                if (navigation.CurrentValue is null) continue;
                if (navigation.CurrentValue is IEnumerable<ISoftDelete> children)
                {
                    foreach (var child in children)
                    {
                        if (!child.IsDeleted)
                        {
                            child.IsDeleted = true;
                            child.DeletedAt = DateTime.UtcNow;
                            CascadeSoftDelete(dbContext, child);
                        }
                    }
                }
                else if (navigation.CurrentValue is ISoftDelete child && !child.IsDeleted)
                {
                    child.IsDeleted = true;
                    child.DeletedAt = DateTime.UtcNow;
                    dbContext.Entry(child).State = EntityState.Modified;
                }
            }
        }
    }
}
