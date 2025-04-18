using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("HISTORICALQUERIES")]
public class HistoricalQuery : BaseEntity
{
    [Column("USER_ID")]
    public int UserId { get; set; }

    [Column("QUERY_PARAMS")]
    public string QueryParams { get; set; }

    [Column("RESPONSE")]
    public string Response { get; set; }

    [Column("QUERIED_AT")]
    public DateTime QueriedAt { get; set; }
}
