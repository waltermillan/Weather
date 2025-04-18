using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories;

public class HistoricalQueryRepository(Data.Context context) : GenericRepository<HistoricalQuery>(context), IHistoricalQueryRepository
{
}
