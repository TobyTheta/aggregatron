using Domain.Common.Services;
using Domain.Platforms;
using Domain.Platforms.Persistence;
using Microsoft.EntityFrameworkCore;
using ValidationResult = Domain.Common.Validation.ValidationResult;

namespace Persistence.TimescaleDb.Platforms;

public class PlatformRepository : Repository<Platform>, IPlatformRepository
{
    public PlatformRepository(AggregatronPersistenceContext persistence) : base(persistence, persistence.Platforms)
    {
    }

    public async Task<Platform?> QueryByIdentifier(string identifier)
    {
        return DbSet.Local.FirstOrDefault(x => x.Identifier == identifier) ??
               await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Identifier == identifier);
    }

    private async Task<Platform?> Find(Platform platform)
    {
        Platform? dbo = null;
        if (platform.Id != 0)
        {
            dbo = await DbSet.FindAsync(platform.Id);
        }

        if (dbo == null)
        {
            dbo = await QueryByIdentifier(platform.Identifier);
        }

        return dbo;
    }

    public async Task<Response<Platform>> CreateOrUpdate(Platform platform)
    {
        var dbo = await Find(platform);
        
        if (dbo != null && dbo.Id != 0)
        {
            dbo.Update(platform);
            DbSet.Entry(dbo).State = EntityState.Modified;
            return new Response<Platform>(dbo, new ValidationResult().AddInfo($"Platform with Identifier {platform.Identifier} already exists.", dbo.Id, nameof(dbo.Identifier)));
        }

        DbSet.Entry(platform).State = EntityState.Added;
        return new Response<Platform>(platform);
    }
}