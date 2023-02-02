using Microsoft.Extensions.Options;
using CloudIn.Core.WebApi.Common.Helpers;
using CloudIn.Core.WebApi.Common.Settings;
using CloudIn.Core.WebApi.GraphQl.Schema.InputTypes;
using CloudIn.Core.Domain.Contracts.Repositories;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Mutations;

[ExtendObjectType(typeof(BaseMutation))]
public class AuthMutation
{
    public record AuthenticationResult(string token);

    //[Error(typeof(UnauthorizedAccessException))]
    public async Task<AuthenticationResult?> Authenticate(
        [Service] IHttpContextAccessor httpContextAccessor,
        [Service] IOptions<AppSettings> settingsProvider,
        [Service] IUserRepository userRepository,
        AuthenticateType authenticatePayload
    )
    {
        var user = await userRepository.GetUserByEmailAsync(authenticatePayload.Username);
        if (user == null || user.Password != authenticatePayload.Password)
        {
            // throw new UnauthorizedAccessException("User not exists or invalid Username/Password");
            throw new GraphQLException("User not exists or invalid Username/Password");
        }

        var settings = settingsProvider.Value;

        var token = TokenHelper.WriteToken(
            secret: settings.AuthenticationJWTSecret,
            expires: settings.AuthenticationExpirationSeconds,
            values: new { Name = user.Id }
        );

        return new(token);
    }
}
