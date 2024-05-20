using Domain.Testing;
using FluentAssertions;
using NUnit.Framework;

namespace Persistence.TimescaleDb.UnitTests.Platforms;

public class PlatformRepositoryTest : RepositoryTest
{
    [Test]
    [Category(TestCategory.IntegrationTests)]
    public void CreatePlatform_ShouldPersistPlatform()
    {
        var platform = PlatformTestData.Platform();
        
        var response = PlatformRepository.CreateOrUpdate(platform).Result;
        PersistenceContext.SaveChanges();
        PersistedPlatforms.Add(platform);
        
        response.Data.Should().NotBeNull();
        response.Data!.Id.Should().NotBe(0);
        platform.Id.Should().NotBe(0);
        
        var platformInDb = PlatformRepository.QueryByIdentifier(platform.Identifier).Result;
        
        platformInDb.Should().NotBeNull();
        platformInDb.Should().BeEquivalentTo(platform);
    }
    
    [Test]
    [Category(TestCategory.IntegrationTests)]
    public void UpdatePlatform_WherePlatformExistsLocal_ShouldPersistUpdate()
    {
        var platform = PlatformTestData.Platform(identifier: "Platform2");
        
        PlatformRepository.CreateOrUpdate(platform).Wait();
        PlatformRepository.CreateOrUpdate(platform).Wait();
        PersistenceContext.SaveChanges();
        PersistedPlatforms.Add(platform);
        
        var platformModified = PlatformTestData.Platform1Modified(identifier: "Platform2");
        
        var response = PlatformRepository.CreateOrUpdate(platformModified).Result;
        PersistenceContext.SaveChanges();
        
        response.Data.Should().NotBeNull();
        response.Data.Should().BeEquivalentTo(platformModified, o => o.Excluding(p => p.Id));
        response.Data!.Id.Should().Be(platform.Id);
        
        ResetPersistenceContext();
        
        var platformInDb = PlatformRepository.QueryByIdentifier(platform.Identifier).Result;
        platformInDb.Should().NotBeNull();
        platformInDb.Should().BeEquivalentTo(platformModified, o => o.Excluding(p => p.Id));
        platformInDb!.Id.Should().Be(platform.Id);
    }
    
    [Test]
    [Category(TestCategory.IntegrationTests)]
    public void UpdatePlatform_WherePlatformExistsInDb_ShouldPersistUpdate()
    {
        var platform = PlatformTestData.Platform(identifier: "Platform3");
        
        PlatformRepository.CreateOrUpdate(platform).Wait();
        PersistenceContext.SaveChanges();
        PersistedPlatforms.Add(platform);

        ResetPersistenceContext();
        
        var platformModified = PlatformTestData.Platform1Modified(identifier: "Platform3");
        
        var response = PlatformRepository.CreateOrUpdate(platformModified).Result;
        PersistenceContext.SaveChangesAsync().Wait();
        
        response.Data.Should().NotBeNull();
        response.Data.Should().BeEquivalentTo(platformModified, o => o.Excluding(p => p.Id));
        response.Data!.Id.Should().Be(platform.Id);
        
        response = PlatformRepository.CreateOrUpdate(response.Data).Result;
        PersistenceContext.SaveChanges();
        
        response.Data.Should().NotBeNull();
        response.Data.Should().BeEquivalentTo(platformModified, o => o.Excluding(p => p.Id));
        response.Data!.Id.Should().Be(platform.Id);
    }

    [Test]
    [Category(TestCategory.IntegrationTests)]
    public void PlatformRepository_PublicGetters_ShouldNotReturnNull()
    {
        PlatformRepository.PersistenceContext.Should().NotBeNull();
    }
}