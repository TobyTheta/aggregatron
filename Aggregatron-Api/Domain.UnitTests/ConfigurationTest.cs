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
    
    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void Test_Encryption_ShouldSucceed()
    {
        var config = ConfigurationHelper.ReadDeveopmentAggregatronConfiguration();

        var s = "SomeTestSecret!";

        var encr = config.Encrypt(s);

        Console.WriteLine($"Encrypted: {encr}");

        var decr = config.Decrypt(encr);

        Console.WriteLine($"Decrypted: {decr}");

        Assert.That(decr, Is.EqualTo(s));
    }
    
    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void Test_Encryption_Hash_ShouldSucceed()
    {
        var config = ConfigurationHelper.ReadDeveopmentAggregatronConfiguration();
        
        var hash1 = config.CreateHash("SomeTestSecret!");

        Console.WriteLine($"Hash 1: {hash1}");

        var hash2 = config.CreateHash("SomeTestSecret!");

        Console.WriteLine($"Decrypted: {hash2}");

        hash1.Should().BeEquivalentTo(hash2);
    }
}