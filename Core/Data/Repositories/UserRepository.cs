using Microsoft.EntityFrameworkCore;
using CloudIn.Core.Domain.Entities;
using CloudIn.Core.Domain.Contracts.Repositories;

namespace CloudIn.Core.Data.Repositories;

public class UserRepository : BaseRepository<UserEntity>, IUserRepository, IAsyncDisposable
{
    public UserRepository(DataContext dbContext) : base(dbContext) { }

    public async Task<UserEntity?> GetUserByEmailAsync(string emailAddress)
    {
        var result = await _context.Users.FirstOrDefaultAsync(user => user.Email == emailAddress);

        return result;
    }

    public ValueTask DisposeAsync() => _context.DisposeAsync();
}
