using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(Data.Context context) : GenericRepository<User>(context), IUserRepository
{
    public Task<User> GetByUserNameAsync(string userName)
    {
        return context.Users
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }
}
