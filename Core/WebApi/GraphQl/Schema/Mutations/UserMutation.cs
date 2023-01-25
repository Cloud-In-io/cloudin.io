// using HotChocolate.AspNetCore.Authorization;
using CloudIn.Core.Domain.Entities;
using CloudIn.Core.Domain.Services.UserService.Interfaces;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Mutations;

[ExtendObjectType(typeof(BaseMutation))]
public class UserMutation
{
    // [Authorize]
    public Task<UserEntity>? CreateUser(
        [Service] IUserService userService,
        ICreateUserPayload createUserPayload
    ) => userService.CreateUserAsync(createUserPayload);
}
