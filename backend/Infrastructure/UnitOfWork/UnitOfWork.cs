using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly Context _context;
    private IUserRepository _users;
    private IHistoricalQueryRepository _historicalQueries;
    private IApiKeyRepository _apyKeys;
    private ICityRepository _cities;

    public UnitOfWork(Context context)
    {
        _context = context;
    }

    public IUserRepository Users
    {
        get
        {
            if (_users is null)
                _users = new UserRepository(_context);

            return _users;
        }
    }

    public IHistoricalQueryRepository HistoricalQueries
    {
        get
        {
            if (_historicalQueries is null)
                _historicalQueries = new HistoricalQueryRepository(_context);

            return _historicalQueries;
        }
    }

    public IApiKeyRepository ApiKeys
    {
        get
        {
            if (_apyKeys is null)
                _apyKeys = new ApiKeyRepository(_context);

            return _apyKeys;
        }
    }

    public ICityRepository Cities
    {
        get
        {
            if (_cities is null)
                _cities = new CityRepository(_context);
            return _cities;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
