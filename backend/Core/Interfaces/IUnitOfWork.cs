namespace Core.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IHistoricalQueryRepository HistoricalQueries { get; }
    IApiKeyRepository ApiKeys { get; }
    ICityRepository Cities { get; }

    void Dispose();
    Task<int> SaveAsync();
}
