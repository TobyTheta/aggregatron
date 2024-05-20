using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Persistence;
using ValidationResult = Domain.Common.Validation.ValidationResult;

namespace Domain.Platforms;

[Table("platform")]
public record Platform : IEntity
{
    public Platform(int id, string identifier, string name, DateTimeOffset? validFrom,
        DateTimeOffset? validTo, bool requiresApiKey, bool requiresApiSecret,
        bool requiresApiPassword, bool hasCryptoApi, bool hasEquitiesApi,
        bool hasUserAccounts, bool allowsTrading)
    {
        Id = id;
        Identifier = identifier;
        Name = name;
        ValidFrom = validFrom;
        ValidTo = validTo;
        RequiresApiKey = requiresApiKey;
        RequiresApiSecret = requiresApiSecret;
        RequiresApiPassword = requiresApiPassword;
        HasCryptoApi = hasCryptoApi;
        HasEquitiesApi = hasEquitiesApi;
        HasUserAccounts = hasUserAccounts;
        AllowsTrading = allowsTrading;
    }

    [Key]
    [Column("id")]
    public int Id { get; init; }
    
    [Required]
    [StringLength(30)]
    [Column("ident")]
    public string Identifier { get; init; }
    
    [Required]
    [StringLength(30)]
    [Column("name")]
    public string Name { get; private set; }
    
    [Column("valid_from")]
    public DateTimeOffset? ValidFrom { get; private set; }

    [Column("valid_to")]
    public DateTimeOffset? ValidTo { get; private set; }
    
    [Column("requires_api_key")]
    public bool RequiresApiKey { get; private set; }
    
    [Column("requires_api_secret")]
    public bool RequiresApiSecret { get; private set; }

    [Column("requires_api_password")]
    public bool RequiresApiPassword { get; private set; }
    
    [Column("has_crypto_api")]
    public bool HasCryptoApi { get; private set; }

    [Column("has_equities_api")]
    public bool HasEquitiesApi { get; private set; }

    [Column("has_user_accounts")]
    public bool HasUserAccounts { get; private set; }

    [Column("allows_trading")]
    public bool AllowsTrading { get; private set; }

    public ValidationResult? Update(Platform other)
    {
        if (Identifier != other.Identifier)
        {
            return new ValidationResult().AddError($"Update failed: Invalid Identifier {other.Identifier}", Id, nameof(Identifier));
        }

        Name = other.Name;
        ValidFrom = other.ValidFrom;
        ValidTo = other.ValidTo;
        RequiresApiKey = other.RequiresApiKey;
        RequiresApiSecret = other.RequiresApiSecret;
        RequiresApiPassword = other.RequiresApiPassword;
        HasCryptoApi = other.HasCryptoApi;
        HasEquitiesApi = other.HasEquitiesApi;
        HasUserAccounts = other.HasUserAccounts;
        AllowsTrading = other.AllowsTrading;
        return null;
    }
}