using Apps.Settings;
using Domain.Platforms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.TimescaleDb.Platforms;

namespace Persistence.TimescaleDb;

public class AggregatronPersistenceContext : TimescaleDbPersistenceContext
{
    public AggregatronPersistenceContext(AggregatronConfiguration configuration) : base(configuration)
    {
    }

    public const string DefaultSchema = "aggregatron";

    //public DbSet<User> Users { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
}

public class AggregatronDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AggregatronPersistenceContext>
{
    public AggregatronPersistenceContext CreateDbContext(string[] args)
    {
        return new AggregatronPersistenceContext(ConfigurationHelper.ReadDeveopmentAggregatronConfiguration());
    }
}