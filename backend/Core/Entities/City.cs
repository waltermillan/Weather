using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("CITIES")]
public class City : BaseEntity
{
    [Column("NAME")]
    public string Name { get; set; }
}
