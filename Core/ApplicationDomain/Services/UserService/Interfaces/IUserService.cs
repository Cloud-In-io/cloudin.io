using ApplicationDomain.Entities;

namespace ApplicationDomain.Services.UserService.Interfaces;

public interface IUserService
{
    Task<UserEntity> CreateUserAsync(ICreateUserPayload createUserPayload);
}
