using Microsoft.EntityFrameworkCore;
using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;

namespace CloudIn.Core.Data.Repositories;

public class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(DataContext context) : base(context) { }

    public async Task<UserEntity?> GetUserByEmailAsync(string emailAddress)
    {
        var result = await _context.Users.FirstOrDefaultAsync(user => user.Email == emailAddress);

        return result;
    }
}
