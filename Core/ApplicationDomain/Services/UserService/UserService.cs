using ApplicationDomain.Entities;
using ApplicationDomain.Services.UserService.Interfaces;

namespace ApplicationDomain.Services.UserService;

public class UserService : IUserService
{
    public UserService() { }

    public Task<UserEntity> CreateUserAsync(ICreateUserPayload createUserPayload)
    {
        throw new NotImplementedException();
    }
}
