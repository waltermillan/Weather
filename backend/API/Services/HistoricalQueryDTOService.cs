using API.DTOs;
using Core.Interfaces;

namespace API.Services;

public class HistoricalQueryDTOService
{
    private readonly IUnitOfWork _unitOfWork;

    public HistoricalQueryDTOService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<HistoricalQueryDTO>> GetHistoricalQueryDTOAsync()
    {
        var historicalQueries = await _unitOfWork.HistoricalQueries.GetAllAsync();

        if (historicalQueries is null)
            return null;

        List<HistoricalQueryDTO> historicalQueryDTOs = new List<HistoricalQueryDTO>();

        foreach (var historicalQuery in historicalQueries)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(historicalQuery.UserId);
            if (user is null)
                return null;

            var historicalQueryDTO = new HistoricalQueryDTO
            {
                Id = historicalQuery.Id,
                UserId = historicalQuery.UserId,
                UserName = user.UserName,
                QueryParams = historicalQuery.QueryParams,
                Response = historicalQuery.Response,
                QueriedAt = historicalQuery.QueriedAt
            };

            historicalQueryDTOs.Add(historicalQueryDTO);
        }
        return historicalQueryDTOs;
    }
}
