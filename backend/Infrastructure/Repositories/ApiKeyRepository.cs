using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApiKeyRepository(Data.Context context) : GenericRepository<ApiKey>(context), IApiKeyRepository
{
    public async Task<ApiKey> GetKeyByProviderAsync(string provider)
    {
        return await _context.ApiKeys.FirstOrDefaultAsync(x => x.Provider == provider);
    }
}
