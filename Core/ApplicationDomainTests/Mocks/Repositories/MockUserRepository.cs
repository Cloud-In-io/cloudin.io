using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.ApplicationDomainTests.Mocks.Repositories;

public class MockUserRepository : IUserRepository
{
    public HashSet<UserEntity> Users { get; private set; } = new();

    public Task AddAsync(UserEntity entity)
    {
        Users.Add(entity);

        return Task.CompletedTask;
    }

    public IEnumerable<UserEntity> GetAll()
    {
        return Users;
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return GetAll();
    }

    public async Task<UserEntity?> GetByIdAsync(Guid Id)
    {
        await Task.Delay(1);

        var user = Users.FirstOrDefault(u => u.Id == Id);

        return user;
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string emailAddress)
    {
        await Task.Delay(1);

        var user = Users.FirstOrDefault(u => u.Email == emailAddress);

        return user;
    }

    public Task RemoveAsync(UserEntity entity)
    {
        Users.Remove(entity);

        return Task.CompletedTask;
    }

    public async Task<bool> SaveChangesAsync()
    {
        await Task.Delay(1);

        return true;
    }
}
