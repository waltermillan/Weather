using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("APIKEYS")]
public class ApiKey : BaseEntity
{
    [Column("USER_ID")]
    public string UserId { get; set; }

    [Column("API_KEY")]
    public string Key { get; set; }

    [Column("PROVIDER")]
    public string Provider { get; set; }

    [Column("CREATED_AT")]
    public DateTime CreatedAt { get; set; }
}
