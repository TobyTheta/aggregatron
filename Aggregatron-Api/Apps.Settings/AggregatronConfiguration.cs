using Microsoft.Extensions.Configuration;

namespace Apps.Settings;

public class AggregatronConfiguration
{
    private const string Section = "Aggregatron";
    
    public Guid AppSecret { get; private set; }
    public string PgConnectionString { get; private set; }
    public Guid AppUserId { get; private set; }
    public string Dataroot { get; private set; }
    
    public bool IsDevelopment { get; private set; }

    public AggregatronConfiguration(IConfiguration configuration, bool isDevelopment)
    {
        var configurationSection = configuration.GetSection(Section);
        AppSecret = Guid.Parse(configurationSection["AppSecret"]!);
        PgConnectionString = configurationSection["PgConnectionString"]!;
        AppUserId = Guid.Parse(configurationSection["AppUserId"]!);
        Dataroot = configurationSection["LocalAppDataRoot"]!;
        IsDevelopment = isDevelopment;
    }
}