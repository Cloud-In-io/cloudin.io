using CloudIn.Core.Domain.Entities;

namespace CloudIn.Core.Domain.Contracts.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task<UserEntity?> GetUserByEmailAsync(string emailAddress);
}
