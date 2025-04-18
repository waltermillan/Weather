using Core.Entities;

namespace Core.Interfaces;

public interface IApiKeyRepository : IGenericRepository<ApiKey>
{
    Task<ApiKey> GetKeyByProviderAsync(string provider);
}
