using CloudIn.Core.Domain.Entities;

namespace CloudIn.Core.Domain.Services.UserService.Interfaces;

public interface IUserService
{
    Task<UserEntity> CreateUserAsync(ICreateUserPayload createUserPayload);
}
