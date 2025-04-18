namespace API.DTOs;

// DTO Pattern: A DTO unifies data from multiple tables into a single object,
//              simplifying data transfer between layers.
public class HistoricalQueryDTO
{
    public int Id { get; set; }                     //Table: HistoricalQueries | Field: Id
    public int UserId { get; set; }                 //Table: HistoricalQueries | Field: UserId
    public string UserName { get; set; }            //Table: Users | Field: UserName
    public string QueryParams { get; set; }         //Table: HistoricalQueries | Field: QueryParams
    public string Response { get; set; }            //Table: HistoricalQueries | Field: Response
    public DateTime QueriedAt { get; set; }         //Table: HistoricalQueries | Field: QueriedAt
}
