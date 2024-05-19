using Microsoft.Extensions.Configuration;

namespace Apps.Settings;

public static class ConfigurationHelper
{
    private static IConfiguration? _config;

    private static AggregatronConfiguration? _aggregatronConfiguration;
    
    public static IConfiguration ReadConfiguration(bool isDevelopment = false)
    {
        if (_config != null)
        {
            return _config;
        }

        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");

        if (isDevelopment)
        {
            builder.AddJsonFile("appsettings.Development.json");
        }

        _config = builder.Build();
        return _config;
    }

    public static AggregatronConfiguration ReadDeveopmentAggregatronConfiguration()
    {
        return _aggregatronConfiguration ??= new AggregatronConfiguration(ReadConfiguration(isDevelopment: true));
    }
}