using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;

namespace CloudIn.Core.ApplicationDomain.Services.UserService;

public class UserService : IUserService
{
    public UserService() { }

    public Task<UserEntity> CreateUserAsync(ICreateUserPayload createUserPayload)
    {
        throw new NotImplementedException();
    }
}
