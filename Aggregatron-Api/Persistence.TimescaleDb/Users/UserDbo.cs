using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.TimescaleDb.Users;

[Table("user")]
public record UserDbo : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; init; }
    
    [Required]
    [Column("uid")]
    public Guid Uid { get; init; }
    
    [Required()]
    [StringLength(40)]
    [Column("first_name")]
    public required string FirstName { get; init; }

    [Required()]
    [StringLength(40)]
    [Column("last_name")]
    public required string LastName { get; init; }

    [Required()]
    [StringLength(40)]
    [Column("email")]
    public required string Email { get; init; }
    
    [Required()]
    [StringLength(512)]
    [Column("pw_hash_encr")]
    public required string PasswordHashEncrypted { get; init; }
}