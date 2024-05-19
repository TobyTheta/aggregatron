using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.TimescaleDb.Platforms;

[Table("platform")]
[Index(nameof(Name), IsUnique = true)]
[Index(nameof(Identifier), IsUnique = true)]
public record PlatformDbo : IEntity
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("id")]
    public int Id { get; init; }
    
    [Required]
    [StringLength(30)]
    [Column("name")]
    public required string Name { get; init; }
    
    [Required]
    [StringLength(30)]
    [Column("ident")]
    public required string Identifier { get; init; }
    
    [Column("valid_from")]
    public DateTimeOffset? ValidFrom { get; init; }

    [Column("valid_to")]
    public DateTimeOffset? ValidTo { get; init; }
    
    [Column("requires_api_key")]
    public bool RequiresApiKey { get; init; }
    
    [Column("requires_api_secret")]
    public bool RequiresApiSecret { get; init; }

    [Column("requires_api_password")]
    public bool RequiresApiPassword { get; init; }
    
    [Column("has_crypto_api")]
    public bool HasCryptoApi { get; init; }

    [Column("has_equities_api")]
    public bool HasEquitiesApi { get; init; }

    [Column("has_user_accounts")]
    public bool HasUserAccounts { get; init; }

    [Column("allows_trading")]
    public bool AllowsTrading { get; init; }
}