using Apps.Settings;
using Domain.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.TimescaleDb;

public abstract class TimescaleDbPersistenceContext : DbContext, IPersistenceContext
{
    protected TimescaleDbPersistenceContext(AggregatronConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.PgConnectionString);

        if (Configuration.IsDevelopment)
        {
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }
    }
    
    public AggregatronConfiguration Configuration { get; private set; }
    
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}