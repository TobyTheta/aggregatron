using Apps.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.TimescaleDb.Users;

namespace Persistence.TimescaleDb;

public class AggregatronDbContext : TimescaleDbPersistenceContext
{
    public AggregatronDbContext(AggregatronConfiguration configuration) : base(configuration)
    {
    }

    public const string DefaultSchema = "aggregatron";

    public DbSet<UserDbo> Users { get; set; } = null!;
}

public class AggregatronDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AggregatronDbContext>
{
    public AggregatronDbContext CreateDbContext(string[] args)
    {
        return new AggregatronDbContext(ConfigurationHelper.ReadDeveopmentAggregatronConfiguration());
    }
}