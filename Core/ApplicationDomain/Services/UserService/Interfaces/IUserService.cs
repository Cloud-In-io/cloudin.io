using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;

public interface IUserService
{
    Task<UserEntity> CreateUserAsync(ICreateUserPayload createUserPayload);
}
