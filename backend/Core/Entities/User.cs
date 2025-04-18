using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("USERS")]
public class User : BaseEntity
{
    [Column("USERNAME")]
    [MaxLength(50)]
    public string UserName { get; set; }

    [Column("PASSWORD")]
    [MaxLength(255)]
    public string Password { get; set; }

    [Column("CREATED_AT")]
    public DateTime CreatedAt { get; set; }
}
