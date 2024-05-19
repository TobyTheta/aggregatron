using Apps.Settings;
using Domain.Testing;
using FluentAssertions;
using NUnit.Framework;
using String = System.String;

namespace Domain.UnitTests;

public class ConfigurationTest
{
    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void ConfigurationHelper_ShouldReadExpectedSettingsFile()
    {
        var aggregatronConfiguration = ConfigurationHelper.ReadDeveopmentAggregatronConfiguration();
        
        aggregatronConfiguration.AppUserId.Should().NotBe(Guid.Empty);
        aggregatronConfiguration.AppSecret.Should().NotBe(Guid.Empty);
        aggregatronConfiguration.PgConnectionString.Should().NotBe(String.Empty);
        aggregatronConfiguration.Dataroot.Should().NotBe(String.Empty);
        
        var aggregatronConfiguration2 = ConfigurationHelper.ReadDeveopmentAggregatronConfiguration();
        aggregatronConfiguration2.Should().BeEquivalentTo(aggregatronConfiguration);

        var configuration = ConfigurationHelper.ReadConfiguration(true);
        configuration.Should().NotBeNull();
    }
}