using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories;

public class CityRepository(Data.Context context) : GenericRepository<City>(context), ICityRepository
{
}
