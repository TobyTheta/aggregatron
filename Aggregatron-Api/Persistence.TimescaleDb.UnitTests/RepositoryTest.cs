using Apps.Settings;
using Domain.Platforms;
using Domain.Testing.TestData;
using NUnit.Framework;
using Persistence.TimescaleDb.Platforms;

namespace Persistence.TimescaleDb.UnitTests;

public abstract class RepositoryTest
{
    protected AggregatronPersistenceContext PersistenceContext = null!;
    protected PlatformRepository PlatformRepository = null!;

    protected readonly List<Platform> PersistedPlatforms = new List<Platform>();
    
    protected readonly PlatformTestData PlatformTestData = new PlatformTestData();

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        PersistenceContext = new AggregatronDesignTimeDbContextFactory().CreateDbContext([]);
        CreateRepositories();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        foreach (var persistedPlatform in PersistedPlatforms)
        {
            PlatformRepository.Delete(persistedPlatform.Id);
        }

        PersistenceContext.SaveChanges();
    }

    protected void ResetPersistenceContext()
    {
        PersistenceContext.Dispose();
        PersistenceContext = new AggregatronDesignTimeDbContextFactory().CreateDbContext([]);
        CreateRepositories();
    }

    private void CreateRepositories()
    {
        PlatformRepository = new PlatformRepository(PersistenceContext);
    }
}