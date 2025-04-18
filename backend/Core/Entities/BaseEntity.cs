using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class BaseEntity
{
    [Column("ID")]
    public int Id { get; set; }
}
