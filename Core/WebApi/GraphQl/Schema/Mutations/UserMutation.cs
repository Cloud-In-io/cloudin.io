using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Mutations;

[ExtendObjectType(typeof(BaseMutation))]
public class UserMutation
{
    public Task<UserEntity> CreateUser(
        [Service] IUserService userService,
        ICreateUserPayload createUserPayload
    ) => userService.CreateUserAsync(createUserPayload);
}
